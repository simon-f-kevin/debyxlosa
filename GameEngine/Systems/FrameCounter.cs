using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Systems
{
    public class FrameCounter
    {
        float frameCount = 0;
        float timeSinceLastUpdate = 0;
        float updateInterval = 1;
        float fps = 0;
        private Game game;
        public FrameCounter(Game game)
        {
            this.game = game;
        }
        public void Update(GameTime gameTime)
        {
            frameCount++;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastUpdate += elapsed;
            if (timeSinceLastUpdate > updateInterval)
            {
                fps = frameCount / timeSinceLastUpdate;
                game.Window.Title = "FPS: " + fps.ToString();
                frameCount = 0;
                timeSinceLastUpdate -= updateInterval;
            }
            //base.Draw(gameTime);
        }
    }
}
