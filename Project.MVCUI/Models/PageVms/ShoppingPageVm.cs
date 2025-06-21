using Project.ENTITIES.Models;
using X.PagedList;

namespace Project.MVCUI.Models.PageVms
{
    public class ShoppingPageVm
    {
        public List<Category> Categories { get; set; }
        public IPagedList<Product> Products { get; set; } //Burada List yerine IPagedList dememizin sebebi göndermek istedigimiz Product listesinin sayfalanabilir bir koleksiyon halinde gönderilmesini istememizdir...Eger List yapsaydık bu ürünler koleksiyonunu View tarafında sayfalara bölemezdik...

    }
}
