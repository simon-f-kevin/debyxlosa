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
    public class AnimationSystem
    {
        SpriteBatch spriteBatch;
        private Game game;
        private float TotalElapsed;
        public AnimationSystem(Game game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
        public void Update(GameTime gameTime)
        {
            Dictionary<int, EntityComponent> _AnimationDict = ComponentManager.Instance.getComponentDictionary<AnimationComponent>();
            Dictionary<int, EntityComponent> _RectangleDict = ComponentManager.Instance.getComponentDictionary<RectangleComponent>();
            EntityComponent rectangleComp;
            TotalElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TotalElapsed > 0.05f)
            {
                TotalElapsed = 0.0f;
                foreach (AnimationComponent animation in _AnimationDict.Values)
                {
                    if (_RectangleDict.TryGetValue(animation.EntityId, out rectangleComp))
                    {
                        RectangleComponent rectangle = (RectangleComponent)rectangleComp;

                        animation.currentFrame++;
                        if (animation.currentFrame == animation.totalFrames)
                        {
                            animation.currentFrame = 0;
                        }
                        int width = animation.Texture.Width / animation.Columns;
                        int height = animation.Texture.Height / animation.Rows;
                        int row = (int)((float)animation.currentFrame / (float)animation.Columns);
                        int column = animation.currentFrame % animation.Columns;
                        animation.sourceRectangle = new Rectangle(width * column, height * row, width, height);
                        rectangle.BoundingRectangle = new Rectangle(rectangle.BoundingRectangle.X, rectangle.BoundingRectangle.Y, width *2, height*2);
                        animation.destinationRectangle = rectangle.BoundingRectangle;
                    }
                }
            }
        }
        public void Draw(GameTime gameTime)
        {
            Dictionary<int, EntityComponent> _AnimationDict = ComponentManager.Instance.getComponentDictionary<AnimationComponent>();
            spriteBatch.Begin();
            foreach (AnimationComponent animation in _AnimationDict.Values)
            {    
             
                    spriteBatch.Draw(texture: animation.Texture,destinationRectangle: animation.destinationRectangle,sourceRectangle: animation.sourceRectangle, color: Color.White);
                    //spriteBatch.Draw(texture: animation.Texture, destinationRectangle: rectangle.BoundingRectangle);
                               
            }
            spriteBatch.End();
        }
    }
}
