using Project.ENTITIES.Models;

namespace Project.MVCUI.Areas.Admin.Models.PageVms
{
    //Bu, verileri tasıyacak bir container sınıfıdır...Model olarak gönderilecek olan bu sınıftır. Bu sayede siz bir Model icerisinde birden fazla veri tasıyabilirsiniz...
    public class ProductPageVm
    {
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }

    }
}
