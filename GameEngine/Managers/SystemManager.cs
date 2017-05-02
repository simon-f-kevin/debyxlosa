using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Util;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers
{
    public class SystemManager
    {
        private List<ISystem> _systems;
        private ISystem _collisionDetectionSystem;
        private InputSystem _InputSystem;
        private MoveSystem _MoveSystem;
        private FrameCounter _FrameCounter;
        private AnimationSystem _animationSystem;
        private CollisionHandlingSystem _CollisionHandler;
        private Game _game;
        public SystemManager(Game game, CollisionHandlingDelegate collisionHandler, SpriteBatch spriteBatch)
        {
            _game = game;
            _systems = new List<ISystem>();
            ISystemsMediator mediator = new CollisionSystemsMediator();


            //_FrameCounter = new FrameCounter(game);
           

            _InputSystem = new InputSystem(game);
           

            _MoveSystem = new MoveSystem(game);
            
            _animationSystem = new AnimationSystem();

            _CollisionHandler = new CollisionHandlingSystem(collisionHandler);
            _collisionDetectionSystem = new CollisionDetectionSystem(mediator);
            ((CollisionSystemsMediator)mediator).CollisionHandler = _CollisionHandler;
        }
       
        public void Update(GameTime gameTime)
        {
            //_FrameCounter.Update(gameTime);
            //_TestSystem.Update(gameTime);
            _InputSystem.Update(gameTime);
            _MoveSystem.Update(gameTime);
            _collisionDetectionSystem.Update(gameTime);
            _CollisionHandler.Update(gameTime);
            _animationSystem.Update(gameTime);
            //_SoundSystem.Update(gameTime);            
            //base.Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationSystem.Draw(spriteBatch);
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            //_FrameCounter.Draw(gameTime);
            //_TextureRender.Draw(gameTime);
            //base.Draw(gameTime);
        }
    }
}
