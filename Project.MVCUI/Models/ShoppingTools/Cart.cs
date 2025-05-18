using Newtonsoft.Json;

namespace Project.MVCUI.Models.ShoppingTools
{
    [Serializable]
    public class Cart
    {
        [JsonProperty("_myCart")]
        readonly Dictionary<int, CartItem> _myCart;

        public Cart()
        {
            _myCart = new Dictionary<int, CartItem>();
        }

        [JsonProperty("GetCartItems")]
        public List<CartItem> GetCartItems
        {
            get
            {
                return _myCart.Values.ToList();
            }
        }

        public void IncreaseCartItem(int id)
        {
            _myCart[id].Amount++;
        }

        public void AddToCart(CartItem item)
        {
            if (_myCart.ContainsKey(item.Id))
            {
                IncreaseCartItem(item.Id);
                return;
            }

            _myCart.Add(item.Id, item);
        }

        public void Decrease(int id)
        {
            _myCart[id].Amount--;
            if (_myCart[id].Amount == 0) RemoveFromCart(id);
        }

        public void RemoveFromCart(int id)
        {
            _myCart.Remove(id);
        }

        [JsonProperty("TotalPrice")]
        public decimal TotalPrice
        {
            get
            {
                return _myCart.Values.Sum(x => x.SubTotal);
            }
        }
    }
}
