using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Systems
{
    public class FrameCounter : DrawableGameComponent
    {
        float frameCount = 0;
        float timeSinceLastUpdate = 0;
        float updateInterval = 1;
        float fps = 0;
        public FrameCounter(Game game) : base(game)
        {
        }
        public override void Draw(GameTime gameTime)
        {
            frameCount++;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastUpdate += elapsed;
            if (timeSinceLastUpdate > updateInterval)
            {
                fps = frameCount / timeSinceLastUpdate;
                Game.Window.Title = "FPS: " + fps.ToString();
                frameCount = 0;
                timeSinceLastUpdate -= updateInterval;
            }
            //base.Draw(gameTime);
        }
    }
}
