using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public interface ISystem
    {
        void Update(GameTime gameTime);
    }
}
