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
        private AISystem _AISystem;
        private TextureRenderSystem _TextureRender;
        private Game game;
        private AnimationSystem _AnimationSystem;
        public SystemManager(Game game) 
        private AnimationSystem _animationSystem;
        private CollisionHandlingSystem _CollisionHandler;
        private Game _game;
        public SystemManager(Game game, CollisionHandlingDelegate collisionHandler, SpriteBatch spriteBatch)
        {
            this.game = game;
        }
        public void Initialize()
        {
            //_TestSystem = new TestSystem(this.Game);
            //Game.Components.Add(_TestSystem);
            _game = game;
            _systems = new List<ISystem>();
            ISystemsMediator mediator = new CollisionSystemsMediator();

            _FrameCounter = new FrameCounter(this.game);


            _InputSystem = new InputSystem(this.game);


            _MoveSystem = new MoveSystem();
            //_FrameCounter = new FrameCounter(game);
           

            _InputSystem = new InputSystem(game);
           

            _CollisionSystem = new CollisionSystem();

            _TextureRender = new TextureRenderSystem(this.game);
            
            _AISystem = new AISystem(this.game);
            _AnimationSystem = new AnimationSystem(this.game);
            //Game.Components.Add(_TextureRender);
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
            _CollisionSystem.Update(gameTime);
            _AISystem.Update(gameTime);
            _AnimationSystem.Update(gameTime);
            //_SoundSystem.Update(gameTime);            
            //base.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            _animationSystem.Draw(spriteBatch);
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            _FrameCounter.Update(gameTime);
            _TextureRender.Draw(gameTime);
            _AnimationSystem.Draw(gameTime);
            //base.Draw(gameTime);
        }
    }
}
