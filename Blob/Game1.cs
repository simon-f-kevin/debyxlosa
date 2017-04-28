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
        private Texture2D ball;

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
            ball = Content.Load<Texture2D>("football");
            
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
            int i = 0;
            
            spriteBatch.Begin();
            foreach (TextureComponent texture in textures)
            {
                PositionComponent entityPosition =
                    ComponentManager.Instance.getComponentByID<PositionComponent>(texture.EntityId);
                RotationComponent entityRotation =
                    ComponentManager.Instance.getComponentByID<RotationComponent>(texture.EntityId);
                Rectangle rect = new Rectangle((int)entityPosition.X, (int)entityPosition.Y, 100, 100);
                spriteBatch.Draw(texture.Sprite, destinationRectangle:rect, color: Color.White, rotation: entityRotation.Rotation, origin: entityRotation.Orgin);
                
            }


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
 
            //player = EntityManager.Instance.createUniqueId();
            //player.addComponent(new PositionComponent(player._entityID , GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2));
            //player.addComponent(new ControllerComponent(player._entityID)); //TODO lägg till mappning av tangenter för styrning
            //player.addComponent(new RotationComponent(player._entityID, 0, hero.Width, hero.Height));
            //player.addComponent(new VelocityComponent(player._entityID, 0, 0));
            //KeyboardControlComponent controll = new KeyboardControlComponent(player._entityID);
            //controll.LeftKey = Keys.Left;
            //controll.UpKey = Keys.Up;
            //controll.DownKey = Keys.Down;
            //controll.RightKey = Keys.Right;
            //player.addComponent(controll);
            //player.addComponent(new ActionDirectionComponent(player._entityID));

            EntityManager.createPlayer(new Vector2(200, 200), new Vector2(0, 0),
                new KeyMappings(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space));
            EntityManager.createPlayer(new Vector2(200, 200), new Vector2(0, 0),
                new KeyMappings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space));

            EntityManager.createDictator(new Vector2(600, 200), new Vector2(1000, 100));

            EntityManager.createAlliance(new Vector2(500, 100), new Vector2(400, 300));
            
            //PositionComponent dictatorPosition = ComponentManager.Instance.getComponentByID<PositionComponent>(entityId);
            //dictatorPosition.X = 350;
            //dictatorPosition.Y = 200;
            //VelocityComponent velocity = ComponentManager.Instance.getComponentByID<VelocityComponent>(entityId);
            //velocity.VelX = 150;
            //velocity.VelY = 50;
            //for (int i = 0; i < 1000; i++)
            //{
            //    EntityManager.createEntity("dictator");
            //}


            ////Enemy
            //enemy = EntityManager.Instance.createUniqueId();
            //enemy.addComponent(new VelocityComponent(enemy._entityID, 200, 300));  //Ställ vinkel och hastighet med hjälp av x och y = smart
            //enemy.addComponent(new PositionComponent(enemy._entityID, 200, 200));  //Startposition x och y

        }
    }
}
