using Project.BLL.CustomManager.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.CustomManager.Concretes
{
    public class CartManager : ICartManager
    {
        public void HandleCart(Kod k)
        {
            switch (k)
            {
                case Kod.Increase:
                    break;
                case Kod.Decrease:
                    break;
                case Kod.Remove:
                    break;
                default:
                    break;
            }
        }
    }
}
