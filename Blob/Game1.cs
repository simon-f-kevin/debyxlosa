using System.Collections.Generic;
using System.Net;
using Blob.Managers;
using Blob.Models;
using Blob.ResourcesProviders;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
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
            hero = Content.Load<Texture2D>("player");
            blob = Content.Load<Texture2D>("dictator");
            
            GamePropertyManager.Instance.setGraphics(this.GraphicsDevice);
            //_moveSystem = new MoveSystem(this,graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            //Components.Add(_moveSystem);

            _SystemManager = new SystemManager(this);
            //Borde ta bort Component.Add(). Systemet anropas manuellt i Update
            Components.Add(_SystemManager);

            //Create a singleton holding the Game-instance instead of sending
            //it as a parameter to appropriate managers.
            GameProvider.getInstance().Game = this;

            //_InputSystem = new InputSystem(this, true);
            //Components.Add(_InputSystem);
            _entityManager = new EntityManager();
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
            //MediaPlayerManager.Instance.addSong(Content.Load<Song>(@"Music\disco"));
            //MediaPlayerManager.Instance.addSong(Content.Load<Song>(@"Music\game"));
            //MediaPlayerManager.Instance.addSong(Content.Load<Song>(@"Music\gameboy"));
            //MediaPlayerManager.Instance.addSong(Content.Load<Song>(@"Music\cloud"));
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
            //var p = (PositionComponent)ComponentManager.Instance.getComponentByID<PositionComponent>(0);
            //var q = (PositionComponent)ComponentManager.Instance.getComponentByID<PositionComponent>(1);
            //var rotation = (RotationComponent)ComponentManager.Instance.getComponentByID<RotationComponent>(0);
            //Rectangle rectangle = new Rectangle(new Point((int)p.X, (int)p.Y), new Point(hero.Width, hero.Height));
            List<TextureComponent> textures = ComponentManager.Instance.getComponentsOfType<TextureComponent>();
            
            
            spriteBatch.Begin();
            foreach (TextureComponent texture in textures)
            {
                PositionComponent entityPosition =
                    ComponentManager.Instance.getComponentByID<PositionComponent>(texture.EntityId);
                RotationComponent entityRotation =
                    ComponentManager.Instance.getComponentByID<RotationComponent>(texture.EntityId);
                RectangleComponent rc = ComponentManager.Instance.getComponentByID<RectangleComponent>(texture.EntityId);
                spriteBatch.Draw(texture.Sprite, new Vector2(rc.BoundingRectangle.X, rc.BoundingRectangle.Y), null, Color.White, entityRotation.Rotation, entityRotation.Orgin, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(rectangle, rc.BoundingRectangle, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void createEntities()
        {
            EntityManager.createPlayer(new Vector2(200, 200), new Vector2(0, 0),
                new KeyMappings(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space));

            EntityManager.createDictator(new Vector2(600, 20), new Vector2(50, -50));
            EntityManager.createDictator(new Vector2(800, 200), new Vector2(50, 50));

        }
    }
}
