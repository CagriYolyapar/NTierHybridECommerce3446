using Project.ENTITIES.Models;
using Project.MVCUI.Models.PaymentApiTools;

namespace Project.MVCUI.Models.PageVms
{
    public class OrderRequestPageVm
    {
        public Order Order { get; set; }
        public PaymentRequestModel PaymentRequestModel { get; set; }
    }
}
