using System.Collections.Generic;
using System.Net;
using Blob.Managers;
using Blob.Models;
using Blob.Models.CollisionHandler;
using Blob.ResourcesProviders;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using GameEngine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Blob
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SystemManager _SystemManager;
        private EntityManager _entityManager;
        private Texture2D hero;
        private Texture2D blob;
        private Texture2D circle;
        private Texture2D rectangle;
        private Texture2D terrorist;
        private Texture2D smileyWalk;
        private FrameCounter _frameCounter;


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
            
            GamePropertyManager.Instance.setGraphics(this.GraphicsDevice);
            //_moveSystem = new MoveSystem(this,graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            //Components.Add(_moveSystem);

            _SystemManager = new SystemManager(this, GameCollisionHandler.CollisionHandler);
            //Borde ta bort Component.Add(). Systemet anropas manuellt i Update
            _frameCounter = new FrameCounter(this);
            //Create a singleton holding the Game-instance instead of sending
            //it as a parameter to appropriate managers.
            GameProvider.getInstance().Game = this;

            //_InputSystem = new InputSystem(this, true);
            //Components.Add(_InputSystem);
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
            //MediaPlayerManager.Instance.addSong(Content.Load<Song>(@"Music\disco"));
            //MediaPlayerManager.Instance.addSong(Content.Load<Song>(@"Music\game"));
            //MediaPlayerManager.Instance.addSong(Content.Load<Song>(@"Music\gameboy"));
            //MediaPlayerManager.Instance.addSong(Content.Load<Song>(@"Music\cloud"));
            EntityManager.getInstance().addTexture("player", Content.Load<Texture2D>("player"));
            EntityManager.getInstance().addTexture("dictator", Content.Load<Texture2D>("dictator"));
            EntityManager.getInstance().addTexture("terrorist", Content.Load<Texture2D>("terrorist"));
            EntityManager.getInstance().addTexture("explosion", Content.Load<Texture2D>("Explosion"));
            EntityManager.getInstance().addTexture("smileyWalk", Content.Load<Texture2D>(@"Animation\boom"));
            EntityManager.getInstance().addTexture("alliance", Content.Load<Texture2D>("alliance"));
            rectangle = Content.Load<Texture2D>("rectangle");
            createEntities();
            //MediaPlayerManager.Instance.Start();
            circle = Content.Load<Texture2D>("circle");
            rectangle = Content.Load<Texture2D>("rectangle");
            

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
            

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //{
            //    Exit();
            //}

            // TODO: Add your update logic here
            _SystemManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _frameCounter.Update(gameTime);
            spriteBatch.Begin();
            _SystemManager.Draw(spriteBatch);
            //List<RectangleComponent> rectangles = ComponentManager.Instance.getComponentsOfType<RectangleComponent>();
            //foreach (RectangleComponent rect in rectangles)
            //{
            //    spriteBatch.Draw(rectangle, rect.BoundingRectangle, null, Color.White);
            //}
            
            spriteBatch.End();

            base.Draw(gameTime);
            
        }

        void createEntities()
        {
            EntityManager.getInstance().createPlayer(new Vector2(200, 200), new Vector2(0, 0),
                new KeyMappings(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space));

            EntityManager.getInstance().createDictator(new Vector2(50, 50), new Vector2(200, 200));
            EntityManager.getInstance().createDictator(new Vector2(750, 50), new Vector2(-200, 200));
            EntityManager.getInstance().createDictator(new Vector2(50, 750), new Vector2(200, -200));
            EntityManager.getInstance().createDictator(new Vector2(750, 750), new Vector2(-200, -200));

            //EntityManager.createTerrorist(new Vector2(500, 500), new Vector2(300, 300));
            //EntityManager.createAnimatedDictator(new Vector2(100, 200), new Vector2(300, 300));
        }


    }
}
