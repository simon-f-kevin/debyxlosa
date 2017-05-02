using GameEngine.Managers;
using GameEngine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blob.Models
{
    public class SplashScreen
    {
        private GraphicsDevice graphicsDevice = GamePropertyManager.Instance.getGraphics();
        private bool everythingIsLoaded;

        public void UpdateSplashScreen(GameTime gameTime)
        {
            everythingIsLoaded = false;

            //load the game assets or just wait some time to show the splash screen

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game1._gameState = GameState.Gameplay;
            }

            everythingIsLoaded = true;

            if (everythingIsLoaded)
            {
                Game1._gameState = GameState.MainMenu;
            }
        }

        public void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Coral);
        }
    }
}