using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Util.Observer
{
    public interface ISystemObserver
    {
         void update(int id, int animationEffect);
    }
}
