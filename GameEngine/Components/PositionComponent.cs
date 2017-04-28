using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class PositionComponent : EntityComponent
    {
        public float X { get; set; }
        public float Y { get; set; }
        public PositionComponent() { }
        public PositionComponent(int id) : base(id) { }

        public PositionComponent(int Id, float x, float y) : base(Id)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
