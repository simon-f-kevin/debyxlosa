using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Util.Mediator;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers
{
    public class SystemManager
    {
        private Queue<ISystem> _systems;
        private ISystem _collisionDetectionSystem;
        private InputSystem _inputSystem;
        private MoveSystem _moveSystem;
        private FrameCounter _frameCounter;
        private TextureRenderSystem _textureRender;
        private AISystem _AISystem;

        private AnimationSystem _animationSystem;
        private CollisionDetectionSystem _collisionDetector;
        private CollisionHandlingSystem _collisionHandler;
        private Game _game;

        public SystemManager(Game game, CollisionHandlingDelegate collisionHandler)
        {
            _game = game;
            Initialize(collisionHandler);
        }
        public void Initialize(CollisionHandlingDelegate collisionHandler)
        {
            //_TestSystem = new TestSystem(this.Game);
            //Game.Components.Add(_TestSystem);
            _systems = new Queue<ISystem>();
            ISystemsMediator mediator = new CollisionSystemsMediator();

            _frameCounter = new FrameCounter(_game);
            _inputSystem = new InputSystem(_game);
            _moveSystem = new MoveSystem();
            _inputSystem = new InputSystem(_game);
            _textureRender = new TextureRenderSystem();
            _animationSystem = new AnimationSystem();
            _collisionHandler = new CollisionHandlingSystem(collisionHandler);
            ((CollisionSystemsMediator)mediator).CollisionHandler = _collisionHandler;
            _collisionDetector = new CollisionDetectionSystem(mediator);
            _AISystem = new AISystem(_game);
        }
        public void Update(GameTime gameTime)
        {
            //_FrameCounter.Update(gameTime);
            //_TestSystem.Update(gameTime);
            _inputSystem.Update(gameTime);
            _moveSystem.Update(gameTime);
            _collisionDetector.Update(gameTime);
            _collisionHandler.Update(gameTime);
            _animationSystem.Update(gameTime);
            _AISystem.Update(gameTime);
            //_SoundSystem.Update(gameTime);            

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _textureRender.Draw(spriteBatch);
            _animationSystem.Draw(spriteBatch);
            
        }
    }
}
