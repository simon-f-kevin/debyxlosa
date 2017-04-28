using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*Not being used*/
//namespace GameEngine.Systems
//{
//    public class ControllerSystem : GameComponent
//    {
//        private GameTime _gameTime;
//        private InputManager im;
  
//        const float acceleration = 1.0f;
//        float friction = 0.005f;  // minska friction så glider spelaren längre

//        private List<ControllerComponent> _controllers;

//        public ControllerSystem(Game game) : base(game)
//        {
//        }

//        public override void Update(GameTime _gameTime)
//        {
//            _controllers = ComponentManager.Instance.getComponentsOfType<ControllerComponent>();
//            var dT = (float)_gameTime.ElapsedGameTime.TotalSeconds;
//            foreach (var controller in _controllers)
//            {
//                VelocityComponent vel = ComponentManager.Instance.getComponentByID<VelocityComponent>(controller.EntityId);
//                PositionComponent pos = ComponentManager.Instance.getComponentByID<PositionComponent>(controller.EntityId);
//                RotationComponent rotation = ComponentManager.Instance.getComponentByID<RotationComponent>(controller.EntityId);
//                im.update();


//                if (im.KeyDown(Keys.Left))
//                {
//                    rotation.rotation -= 5.0f * dT;
//                }
//                if (im.KeyDown(Keys.Right))
//                {
//                    rotation.rotation += 5.0f * dT;
//                }
//                if (im.KeyDown(Keys.Up))
//                {
//                    vel.VelX += (float)Math.Cos(rotation.rotation) * acceleration;
//                    vel.VelY += (float)Math.Sin(rotation.rotation) * acceleration;
//                }
//                //bromsar ner spelaren
//                else if (vel.VelX != 0 || vel.VelY != 0)
//                {
//                    float i = vel.VelX;
//                    float j = vel.VelY;

//                    vel.VelX = i -= friction * i;
//                    vel.VelY = j -= friction * j;
//                }
            

//                //Håll spelare innanför fönsterkant
//                //if (pos.X > _graphicsDevice.Viewport.Width - 75)  //TODO ändra till spritebredd
//                //    pos.X = _graphicsDevice.Viewport.Width - 75;
//                //if (pos.X < 0)
//                //    pos.X = 0;
//                //if (pos.Y > _graphicsDevice.Viewport.Height - 100)
//                //    pos.Y = _graphicsDevice.Viewport.Height - 100;
//                //if (pos.Y < 0)
//                //    pos.Y = 0;
//            }
//        }
//    }
//}
