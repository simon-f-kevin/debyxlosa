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
        public float Rotation { get; set; }
        public Vector2 Orgin { get; set; }
        public RotationComponent() { }
        public RotationComponent(int id) : base(id) { }
        public RotationComponent(int Id, float rotation, float x, float y) : base(Id)
        {
            Rotation = rotation;
            Orgin = new Vector2(x/2, y/2);
        }

        public void setValues(float x, float y, float rotation)
        {
            Rotation = rotation;
            Orgin = new Vector2(x/2, y/2);
        }
    }
}
