using System.Net.Mime;
using GameEngine.Managers;
using GameEngine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blob.Models
{
    public class MainMenu
    {
        private GraphicsDevice graphicsDevice = GamePropertyManager.Instance.getGraphics();
        private Keys _startButton = Keys.Space;
        
        private string gameName = "Blob Wars";

        private SpriteBatch spriteBatch;

        public void Update(GameTime gameTime)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            //load the game assets or just wait some time to show the splash screen

            if (Keyboard.GetState().IsKeyDown(_startButton))
            {
                Game1._gameState = GameState.Gameplay;
            }

        }

        public void Draw(GameTime gameTime, SpriteFont font)
        {
            graphicsDevice.Clear(Color.Black);
            Viewport viewport = graphicsDevice.Viewport;
            Vector2 stringLen = font.MeasureString(gameName);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, gameName, new Vector2((viewport.Width - stringLen.X)/2, 10), Color.CornflowerBlue);
            spriteBatch.End();
        }
    }
}