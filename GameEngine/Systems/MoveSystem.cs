using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class MoveSystem: GameComponent
    {
        private int _ScreenWitdh { get; }
        private int _ScreenHight { get; }
        private const float acceleration = 1.0f;
        private float friction = 0.0005f;
        private List<VelocityComponent> _velocities;


        public MoveSystem(Game game, int screenWidth, int screenHight) : base(game)
        {
            this._ScreenHight = screenHight;
            this._ScreenWitdh = screenWidth;
        }

        public override void Update(GameTime _gameTime)
        {
            _velocities = ComponentManager.Instance.getComponentsOfType<VelocityComponent>();
            //använd _gameTime
            var dT = (float)_gameTime.ElapsedGameTime.TotalSeconds;
            if (_velocities != null)
            {
                foreach (VelocityComponent vel in _velocities)
                {
                    PositionComponent pos = ComponentManager.Instance.getComponentByID<PositionComponent>(vel.EntityId);
                    ActionDirectionComponent actiondir = ComponentManager.Instance.getComponentByID<ActionDirectionComponent>(vel.EntityId);                   
                    if (actiondir != null)
                    {
                        RotationComponent rotation = ComponentManager.Instance.getComponentByID<RotationComponent>(vel.EntityId);
                        if (actiondir.Left)
                        {
                            rotation.rotation -= 5.0f * dT;
                            actiondir.Left = false;
                        }
                        if (actiondir.Right)
                        {
                            rotation.rotation += 5.0f * dT;
                            actiondir.Right = false;
                        }
                        if (actiondir.Up)
                        {

                            vel._velX += (float)Math.Cos(rotation.rotation - 90f) * acceleration;
                            vel._velY += (float)Math.Sin(rotation.rotation - 90f) * acceleration;
                            //actiondir.Up = false;
                        }
                        else if (vel._velX != 0 || vel._velY != 0)
                        {
                            float i = vel._velX;
                            float j = vel._velY;

                            vel._velX = i -= friction * i;
                            vel._velY = j -= friction * j;
                        }
                    }
                        //Förflyttning
                        pos.X += (vel._velX * dT);
                        pos.Y += (vel._velY * dT);


                        //Håll Entitet innanför fönsterkanter
                        if (pos.X > _ScreenWitdh)  //TODO ändra till spritebredd
                            pos.X = _ScreenWitdh;
                        if (pos.X < 0)
                            pos.X = 0;
                        if (pos.Y > _ScreenHight)
                            pos.Y = _ScreenHight;
                        if (pos.Y < 0)
                            pos.Y = 0;

                        //Om kollision med kant byt vinkel
                        if (pos.X >= _ScreenWitdh || pos.X <= 0)
                        {
                            vel._velX *= -1;
                        }

                        if (pos.Y >= _ScreenHight || pos.Y <= 0)
                        {
                            vel._velY *= -1;
                        }
                }
            }         
        }
    }
}

//Använd med System systemService = new MoveSystem(gameTime);
