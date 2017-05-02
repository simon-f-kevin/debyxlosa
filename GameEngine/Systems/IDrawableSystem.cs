using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public interface IDrawableSystem
    {
        void Draw(SpriteBatch spriteBatch);
    }
}