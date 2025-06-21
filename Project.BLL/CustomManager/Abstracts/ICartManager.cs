using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.CustomManager.Abstracts
{
    public interface ICartManager
    {
        void HandleCart(Kod k);
    }


    public enum Kod
    {
        Increase,
        Decrease,
        Remove
    }
}
