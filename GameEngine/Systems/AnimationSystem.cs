using System;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class AnimationSystem : ISystem, IDrawableSystem
    {
        public AnimationSystem()
        { }

        public void Update(GameTime gameTime)
        {
            List<AnimationComponent> _animations = ComponentManager.Instance.getComponentsOfType<AnimationComponent>();
            if (_animations != null)
            {
                foreach (AnimationComponent animation in _animations)
                {
                    animation.TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                    if (animation.TimeSinceLastFrame > animation.MillisecondsPerFrame)
                    {
                        animation.TimeSinceLastFrame -= animation.MillisecondsPerFrame;
                        Point curreFrame = animation.CurrentFrame;
                        ++curreFrame.X;
                        if (curreFrame.X >= animation.SheetSize.X)
                        {
                            curreFrame.X = 0;
                            ++curreFrame.Y;
                            if (curreFrame.Y >= animation.SheetSize.Y)
                            {
                                curreFrame.Y = 0;
                                animation.notify(animation.EntityId, animation.AnimationEffect);
                            }
                        }
                        animation.CurrentFrame = curreFrame;
                    }
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Dictionary<int, EntityComponent> _positions = ComponentManager.Instance.getComponentDictionary<PositionComponent>();
            Dictionary<int, EntityComponent> _animations = ComponentManager.Instance.getComponentDictionary<AnimationComponent>();
            Dictionary<int, EntityComponent> _scales = ComponentManager.Instance.getComponentDictionary<ScaleComponent>();
            EntityComponent posComponent;
            if (_animations != null)
            {
                foreach (AnimationComponent animation in _animations.Values)
                {
                    if (_positions.TryGetValue(animation.EntityId, out posComponent))
                    {
                        PositionComponent pos = (PositionComponent)posComponent;
                        ScaleComponent scale = (ScaleComponent)_scales[animation.EntityId];
                        spriteBatch.Draw(animation.SpriteSheet, new Vector2(pos.X, pos.Y),
                            new Rectangle(animation.CurrentFrame.X * animation.FrameSize.X, animation.CurrentFrame.Y * animation.FrameSize.Y,
                            animation.FrameSize.X, animation.FrameSize.Y), Color.White, 0, Vector2.Zero,scale.scale, SpriteEffects.None, 0);
                        //if(fullRun){notify all observers)
                    }
                }
            }
        }
    }
}
//using GameEngine.Components;
//using GameEngine.Managers;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GameEngine.Systems
//{
//    public class AnimationSystem
//    {
//        SpriteBatch spriteBatch;
//        private Game game;
//        private float TotalElapsed;
//        public AnimationSystem(Game game)
//        {
//            this.game = game;
//            spriteBatch = new SpriteBatch(game.GraphicsDevice);
//        }
//        public void Update(GameTime gameTime)
//        {
//            Dictionary<int, EntityComponent> _AnimationDict = ComponentManager.Instance.getComponentDictionary<TestAnimationComponent>();
//            Dictionary<int, EntityComponent> _RectangleDict = ComponentManager.Instance.getComponentDictionary<RectangleComponent>();
//            EntityComponent rectangleComp;
//            TotalElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
//            if (TotalElapsed > 0.05f)
//            {
//                TotalElapsed = 0.0f;
//                foreach (TestAnimationComponent animation in _AnimationDict.Values)
//                {
//                    if (_RectangleDict.TryGetValue(animation.EntityId, out rectangleComp))
//                    {
//                        RectangleComponent rectangle = (RectangleComponent)rectangleComp;

//                        animation.currentFrame++;
//                        if (animation.currentFrame == animation.totalFrames)
//                        {
//                            animation.currentFrame = 0;
//                        }
//                        int width = animation.Texture.Width / animation.Columns;
//                        int height = animation.Texture.Height / animation.Rows;
//                        int row = (int)((float)animation.currentFrame / (float)animation.Columns);
//                        int column = animation.currentFrame % animation.Columns;
//                        animation.sourceRectangle = new Rectangle(width * column, height * row, width, height);
//                        rectangle.BoundingRectangle = new Rectangle(rectangle.BoundingRectangle.X, rectangle.BoundingRectangle.Y, width * 2, height * 2);
//                        animation.destinationRectangle = rectangle.BoundingRectangle;
//                    }
//                }
//            }
//        }
//        public void Draw(SpriteBatch spritebatch)
//        {
//            Dictionary<int, EntityComponent> _AnimationDict = ComponentManager.Instance.getComponentDictionary<AnimationComponent>();

//            foreach (AnimationComponent animation in _AnimationDict.Values)
//            {

//                spriteBatch.Draw(texture: animation.SpriteSheet, destinationRectangle: animation.destinationRectangle, sourceRectangle: animation.sourceRectangle, color: Color.White);
//                //spriteBatch.Draw(texture: animation.Texture, destinationRectangle: rectangle.BoundingRectangle);

//            }

//        }
//    }
//}

