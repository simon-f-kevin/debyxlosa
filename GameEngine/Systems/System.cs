using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Managers;

namespace GameEngine.Systems
{
    public abstract class System<T, TT>
    {
        
        public System(T type, TT type2)
        {
            
        }

        public abstract void Update();
    }
}

