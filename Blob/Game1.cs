using System.Net;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blob
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        float frameCount = 0;
        float timeSinceLastUpdate = 0;
        float updateInterval = 1;
        float fps = 0;

        private MoveSystem _moveSystem;
        private InputSystem _InputSystem;

        private Entity player, enemy;
        private Texture2D hero;
        private Texture2D blob;

        public Game1()
        {
            IsFixedTimeStep = false;
            graphics = new GraphicsDeviceManager(this);
            this.IsFixedTimeStep = false;
            this.graphics.SynchronizeWithVerticalRetrace = false;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            hero = Content.Load<Texture2D>("hero1");
            blob = Content.Load<Texture2D>("enemy");
            _moveSystem = new MoveSystem(this,graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Components.Add(_moveSystem);

            _InputSystem = new InputSystem(this, true);
            Components.Add(_InputSystem);
            createEntities();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);



            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Hjälp för debugging
            frameCount++;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastUpdate += elapsed;
            if (timeSinceLastUpdate > updateInterval)
            {
                fps = frameCount / timeSinceLastUpdate;
                Window.Title = "FPS: " + fps.ToString();
                frameCount = 0;
                timeSinceLastUpdate -= updateInterval;
            }

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //{
            //    Exit();
            //}

            // TODO: Add your update logic here
            _InputSystem.Update(gameTime);
            _moveSystem.Update(gameTime);
            //base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            var p = (PositionComponent)ComponentManager.Instance.getComponentByID<PositionComponent>(player._entityID);
            var q = (PositionComponent)ComponentManager.Instance.getComponentByID<PositionComponent>(enemy._entityID);
            var rotation = (RotationComponent)ComponentManager.Instance.getComponentByID<RotationComponent>(player._entityID);
            Rectangle rectangle = new Rectangle(new Point((int)p.X, (int)p.Y), new Point(hero.Width, hero.Height));

            spriteBatch.Begin();
            spriteBatch.Draw(hero, new Vector2(p.X, p.Y), null, Color.White, rotation.rotation, rotation.orgin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(blob, new Vector2(q.X, q.Y), null, Color.White, 0, new Vector2(), 1, SpriteEffects.None, 0);
            spriteBatch.End();
           

            base.Draw(gameTime);
        }

        void createEntities()
        {
            //EM = EntityManager.Instance;
            //Sprite humanSprite = new Sprite("hero1");
            //humanSprite.X = 100;
            //humanSprite.Y = 100;

            

            //player1 = EM.createUniqueId();
            //player1.addComponent(new RenderComponent(humanSprite));
            //Player
 
            player = EntityManager.Instance.createUniqueId();
            player.addComponent(new PositionComponent(player._entityID , GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2));
            player.addComponent(new ControllerComponent(player._entityID)); //TODO lägg till mappning av tangenter för styrning
            player.addComponent(new RotationComponent(player._entityID, 0, hero.Width, hero.Height));
            player.addComponent(new VelocityComponent(player._entityID, 0, 0));
            KeyboardControllComponent controll = new KeyboardControllComponent(player._entityID);
            controll.LeftKey = Keys.Left;
            controll.UpKey = Keys.Up;
            controll.DownKey = Keys.Down;
            controll.RightKey = Keys.Right;
            player.addComponent(controll);
            player.addComponent(new ActionDirectionComponent(player._entityID));

            //Enemy
            enemy = EntityManager.Instance.createUniqueId();
            enemy.addComponent(new VelocityComponent(enemy._entityID, 200, 300));  //Ställ vinkel och hastighet med hjälp av x och y = smart
            enemy.addComponent(new PositionComponent(enemy._entityID, 200, 200));  //Startposition x och y

        }
    }
}
