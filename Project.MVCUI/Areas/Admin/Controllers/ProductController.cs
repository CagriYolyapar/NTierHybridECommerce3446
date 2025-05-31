using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Managers.Abstracts;
using Project.BLL.Managers.Concretes;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.Models.PageVms;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize("Admin")] //BU sekilde bir Authorization vermeniz bir police ayarlamanız demektir...Demek ki Admin isminde özel bir yetki sistemi ayarlamanız var ve onu kullanmak istiyorsunuz demektir
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        readonly IProductManager _productManager;
        readonly ICategoryManager _categoryManager;

        public ProductController(IProductManager productManager,ICategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }

       

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productManager.GetAllAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            ProductPageVm pVm = new()
            {
                Categories = _categoryManager.GetActives(),

            };
            return View(pVm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductPageVm pvm,IFormFile formFile)
        {
            //Dosya yolu olusturma resim yükleme vs...
            #region ResimKodlari 

            Guid uniqueName = Guid.NewGuid();

            string extension = Path.GetExtension(formFile.FileName); //dosyanın uzantısını aldık

            //if(extension != "png" || extension != "jpeg" || extension != "gif")
            pvm.Product.ImagePath = $"/images/{uniqueName}.{extension}";

            string path = $"{Directory.GetCurrentDirectory()}/wwwroot/{pvm.Product.ImagePath}";
            FileStream stream = new(path, FileMode.Create); //path'i verdikten sonra Create ile o ilgili path'te dosya yaratıyoruz...
            formFile.CopyTo(stream);

            #endregion

            await _productManager.CreateAsync(pvm.Product);
            return RedirectToAction("Index");
        }
    }
}
