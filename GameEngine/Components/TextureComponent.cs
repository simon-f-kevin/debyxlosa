using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class TextureComponent : EntityComponent
    {
        private Texture2D _sprite;

        public Texture2D Sprite
        {
            get { return _sprite; }
        }
        public TextureComponent() { }
        public TextureComponent(int id) :base(id) { }
        public TextureComponent(int Id, Texture2D sprite) : base(Id)
        {
            _sprite = sprite;
        }
    }
}