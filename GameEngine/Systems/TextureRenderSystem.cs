using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Systems
{
    public class TextureRenderSystem : DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        public TextureRenderSystem(Game game) : base(game)
        {
        }
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //base.Initialize();
        }
        //public override void Draw(GameTime gameTime)
        //{
        //    Dictionary<int, EntityComponent> _RectangleDictionary = ComponentManager.Instance getComponentDictionary<RectangleComponent>();
        //    Dictionary<int, EntityComponent> _TexturesDictionary = ComponentManager.Instance.getComponentDictionary<Texture2DComponent>();
        //    EntityComponent component;
        //    if (_TexturesDictionary != null && _RectangleDictionary != null)
        //    {
        //        spriteBatch.Begin();
        //        foreach (Texture2DComponent texture2D in _TexturesDictionary.Values)
        //        {
        //            if (_RectangleDictionary.TryGetValue(texture2D.EntityID, out component))
        //            {
        //                RectangleComponent rectangle = (RectangleComponent)component;
        //                spriteBatch.Draw
        //                    (
        //                        texture: texture2D.texture,
        //                        destinationRectangle: rectangle.rectangle,
        //                        color: Color.White
        //                    );
        //            }
        //        }

        //        spriteBatch.End();
        //    }
        //    //base.Draw(gameTime);
        //}
    }
}
