using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class RectangleComponent : EntityComponent
    {
        //public Vector2 _rectangle { get; set; }
        public Rectangle BoundingRectangle { get; set; }
        public RectangleComponent() { }
        public RectangleComponent(int id) : base(id) { }

        public RectangleComponent(int id, Rectangle bounds) : base(id)
        {
            BoundingRectangle = bounds;
        }

    }
}
