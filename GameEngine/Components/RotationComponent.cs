using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class RotationComponent : EntityComponent
    {
        public float rotation { get; set; }
        public Vector2 orgin { get; set; }
        public RotationComponent(int Id, float rotation, float x, float y) : base(Id)
        {
            this.rotation = rotation;
            orgin = new Vector2(x/2, y/2);
        }
    }
}
