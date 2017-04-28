using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class TextureComponent : EntityComponent
    {

        public Texture2D Sprite { get; set; }
      
        public TextureComponent() { }
        public TextureComponent(int id) :base(id) { }
        public TextureComponent(int Id, Texture2D sprite) : base(Id)
        {
            Sprite = sprite;
        }

        public void setValue(Texture2D sprite)
        {
            Sprite = sprite;
        }
    }
}