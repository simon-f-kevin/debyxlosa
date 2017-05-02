using System;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class AnimationSystem : ISystem , IDrawableSystem
    {
        private List<AnimationComponent> _animations;

        public AnimationSystem()
        {}

        public void Update(GameTime gameTime)
        {
            _animations = ComponentManager.Instance.getComponentsOfType<AnimationComponent>();
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
                                curreFrame.Y = 0;
                        }
                        animation.CurrentFrame = curreFrame;
                    }
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Dictionary<int, EntityComponent> _positions =
                ComponentManager.Instance.getComponentDictionary<PositionComponent>();
            if (_animations != null)
            {
                foreach (AnimationComponent animation in _animations)
                {
                    PositionComponent pos = (PositionComponent)_positions[animation.EntityId];
                    spriteBatch.Draw(animation.SpriteSheet, new Vector2(pos.X, pos.Y),
                        new Rectangle(animation.CurrentFrame.X * animation.FrameSize.X, animation.CurrentFrame.Y * animation.FrameSize.Y,
                        animation.FrameSize.X, animation.FrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }
    }
}