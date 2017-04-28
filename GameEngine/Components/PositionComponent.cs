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

        public PositionComponent()
        {
            X = 0;
            Y = 0;
        }

        public PositionComponent(int id) : base(id)
        {
            X = 0;
            Y = 0;
        }

        public PositionComponent(int Id, float x, float y) : base(Id)
        {
            this.X = x;
            this.Y = y;
        }

        public void setValues(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
