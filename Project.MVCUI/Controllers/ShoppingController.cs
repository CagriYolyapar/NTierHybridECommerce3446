using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.BLL.Managers.Abstracts;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.PageVms;
using Project.MVCUI.Models.SessionService;
using Project.MVCUI.Models.ShoppingTools;
using System.Text;
using X.PagedList;
using X.PagedList.Extensions;


namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        readonly IProductManager _productManager;
        readonly ICategoryManager _categoryManager;
        readonly IOrderManager _orderManager;
        readonly UserManager<AppUser> _userManager;
        readonly IHttpClientFactory _httpClientFactory; //Middleware'de HttpClient servisi tanımlı ise artık orası sayesinde constructor'imiz bu tipi inject edebilir...
        readonly IOrderDetailManager _orderDetailManager;
        public ShoppingController(IProductManager productManager, ICategoryManager categoryManager, IOrderManager orderManager, UserManager<AppUser> userManager, IHttpClientFactory httpClientFactory, IOrderDetailManager orderDetailManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index(int? page, int? categoryId)
        {
            //string a = "Cagrı";

            //string b = a ?? "Deneme"; //eger a null ise (a??) Deneme degerini b isimli degişkene at, eger a'nın degeri varsa a'nın degerini b isimli degişkene at.
            List<Product> products = categoryId == null ? _productManager.GetActives() : _productManager.Where(x => x.CategoryId == categoryId);

            IPagedList<Product> pagedProducts = products.ToPagedList(page ?? 1, 5);//page degeri null ise 1. sayfadan baslasın null degilse page degeri kacsa o sayfadan baslasın...İkinci argümanımız ise (5) bir sayfada kac ürün bulundurulsun...

            List<Category> categories = _categoryManager.GetActives();

            ShoppingPageVm spVm = new()
            {
                Categories = categories,
                Products = pagedProducts
            };

            if (categoryId != null) TempData["catId"] = categoryId;

            return View(spVm);
        }

        Cart GetCartFromSession(string key)
        {
            return HttpContext.Session.GetObject<Cart>(key);
        }

        void SetCartForSession(Cart c)
        {
            HttpContext.Session.SetObject("scart", c);
        }

        void ControlCart(Cart c)
        {
            if (c.GetCartItems.Count == 0) HttpContext.Session.Remove("scart");
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            Cart c = GetCartFromSession("scart") == null ? new Cart() : GetCartFromSession("scart");

            Product eklenecekUrun = await _productManager.GetByIdAsync(id); //elimize kullanıcının eklemek istedigi ürün

            CartItem ci = new()
            {
                Id = eklenecekUrun.Id,
                ProductName = eklenecekUrun.ProductName,
                UnitPrice = eklenecekUrun.UnitPrice,
                ImagePath = eklenecekUrun.ImagePath,
                CategoryId = eklenecekUrun.CategoryId,
                CategoryName = eklenecekUrun.Category == null ? "Kategorisi yok" : eklenecekUrun.Category.CategoryName
            };

            c.AddToCart(ci);

            SetCartForSession(c);
            TempData["Message"] = $"{ci.ProductName} isimli ürün sepete eklenmiştir";
            return RedirectToAction("Index");
        }

        public IActionResult CartPage()
        {
            if (GetCartFromSession("scart") == null)
            {
                TempData["Message"] = "Sepetiniz su anda bos";
                return RedirectToAction("Index");
            }
            Cart c = GetCartFromSession("scart");
            return View(c);
        }

        //Todo Odev : RemoveFromCart, DecreaseFromCart ve IncreaseFromCart ACtion'ları bir CustomManager ile Ram icerisindeki bir iş akısında cok benzer mantıga sahip oldugu icin refactor edilmelidir...

        public IActionResult RemoveFromCart(int id)
        {
            if (GetCartFromSession("scart") != null)
            {
                Cart c = GetCartFromSession("scart");
                c.RemoveFromCart(id);
                SetCartForSession(c);
                ControlCart(c);
            }

            return RedirectToAction("CartPage");
        }

        public IActionResult DecreaseFromCart(int id)
        {
            if (GetCartFromSession("scart") != null)
            {
                Cart c = GetCartFromSession("scart");
                c.Decrease(id);
                SetCartForSession(c);
                ControlCart(c);
            }
            return RedirectToAction("CartPage");
        }

        public IActionResult IncreaseFromCart(int id)
        {
            if (GetCartFromSession("scart") != null)
            {
                Cart c = GetCartFromSession("scart");
                c.IncreaseCartItem(id);
                SetCartForSession(c);
                ControlCart(c);
            }
            return RedirectToAction("CartPage");
        }



        public IActionResult ConfirmOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(OrderRequestPageVm ovm)
        {
            Cart c = GetCartFromSession("scart");
            ovm.Order.Price = ovm.PaymentRequestModel.ShoppingPrice = c.TotalPrice;

            #region APIIntegration
            //http://localhost:5292/api/Transaction

            //API'yı Consume etmek icin middleware'de ilk basta bir ayarlama yapmak zorundasınız...

            HttpClient client = _httpClientFactory.CreateClient();
            string jsonData = JsonConvert.SerializeObject(ovm.PaymentRequestModel);

            StringContent content = new(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await client.PostAsync("http://localhost:5292/api/Transaction", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                if (User.Identity.IsAuthenticated)
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    ovm.Order.AppUserId = appUser.Id;
                }

                await _orderManager.CreateAsync(ovm.Order); //Bu ifadenin kesinlikle önceden söylenmesi gerekiyor.Cünkü Order nesnesinin Id'sinin olusması icin Order nesnesinin veritbananına eklenmesi lazım. Onun Id'sine ihtiyacımız var ki onu OrderDetails tablosuna id'si ile ekleyebilelim...
                foreach(CartItem item in c.GetCartItems)
                {
                    OrderDetail od = new();
                    od.OrderId = ovm.Order.Id;
                    od.ProductId = item.Id;
                    od.Quantity = item.Amount;
                    od.UnitPrice = item.UnitPrice;

                    await _orderDetailManager.CreateAsync(od);
                }
                TempData["Message"] = "Siparişiniz bize basarıyla ulasmıstır..Tesekkür ederiz";
                HttpContext.Session.Remove("scart"); //Session'i silme kodu
                return RedirectToAction("Index");
            }

            string result = await responseMessage.Content.ReadAsStringAsync();
            TempData["Message"] = result;
            return RedirectToAction("Index");

            #endregion
        }
    }
}
