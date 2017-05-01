using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class AnimationComponent: EntityComponent
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame;
        public int totalFrames;
        public Rectangle sourceRectangle { get; set; }
        public Rectangle destinationRectangle { get; set; }
        public AnimationComponent()
        {

        }
        public AnimationComponent(int id) :base(id)
        {

        }
    }
}
