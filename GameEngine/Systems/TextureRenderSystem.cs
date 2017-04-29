using GameEngine.Components;
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
    public class TextureRenderSystem
    {
        SpriteBatch spriteBatch;
        private Game game;
        public TextureRenderSystem(Game game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void Draw(GameTime gameTime)
        {
            Dictionary<int, EntityComponent> _RectangleDictionary = ComponentManager.Instance.getComponentDictionary<RectangleComponent>();
            Dictionary<int, EntityComponent> _TexturesDictionary = ComponentManager.Instance.getComponentDictionary<TextureComponent>();
            Dictionary<int, EntityComponent> _RotationDictionary = ComponentManager.Instance.getComponentDictionary<RotationComponent>();
            Dictionary<int, EntityComponent> _PositionDictionary = ComponentManager.Instance.getComponentDictionary<PositionComponent>();
            EntityComponent rectangelComponent;
            EntityComponent rotationComponent;
            EntityComponent posComponent;
            if (_TexturesDictionary != null && _RectangleDictionary != null && _RotationDictionary != null)
            {
                spriteBatch.Begin();
                foreach (TextureComponent texture2D in _TexturesDictionary.Values)
                {
                    if (_RectangleDictionary.TryGetValue(texture2D.EntityId, out rectangelComponent))
                    {
                        RectangleComponent rectangle = (RectangleComponent)rectangelComponent;
                        if (_RotationDictionary.TryGetValue(texture2D.EntityId, out rotationComponent))
                        {
                            RotationComponent rotation = (RotationComponent)rotationComponent;
                            spriteBatch.Draw
                                (
                                    texture: texture2D.Sprite,
                                    destinationRectangle: rectangle.BoundingRectangle,
                                    color: Color.White,
                                    rotation: rotation.Rotation,
                                    origin: rotation.Orgin                               
                                );
                        }
                        else
                        {
                            spriteBatch.Draw
                                (
                                    texture: texture2D.Sprite,
                                    destinationRectangle: rectangle.BoundingRectangle,
                                    color: Color.White
                                );
                        }
                    }
                    else
                    {
                        if(_PositionDictionary.TryGetValue(texture2D.EntityId, out posComponent))
                        {
                            PositionComponent position = (PositionComponent)posComponent;
                            spriteBatch.Draw
                                (
                                    texture: texture2D.Sprite,
                                    position: new Vector2(position.X, position.Y),
                                    color: Color.White
                                );
                        }

                    }
                }

                spriteBatch.End();
            }
        }
        //List<TextureComponent> textures = ComponentManager.Instance.getComponentsOfType<TextureComponent>();
        //spriteBatch.Begin();
        //    foreach (TextureComponent texture in textures)
        //    {
        //        RotationComponent entityRotation =
        //            ComponentManager.Instance.getComponentByID<RotationComponent>(texture.EntityId);
        //RectangleComponent rc = ComponentManager.Instance.getComponentByID<RectangleComponent>(texture.EntityId);
        //spriteBatch.Draw(texture.Sprite, new Vector2(rc.BoundingRectangle.X, rc.BoundingRectangle.Y), null, Color.White, entityRotation.Rotation, entityRotation.Orgin, 1f, SpriteEffects.None, 0f);
        //        spriteBatch.Draw(rectangle, rc.BoundingRectangle, Color.White);
        //    }
}
}
