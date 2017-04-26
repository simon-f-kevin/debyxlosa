using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class VelocityComponent : EntityComponent
    {
        public float _velX { get; set; }
        public float _velY  { get; set; }

        public VelocityComponent(int Id, float x, float y) : base(Id)
        {
            _velX = x;
            _velY = y;
        }
    }
}
