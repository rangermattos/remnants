using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Remnants
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        MenuController menuController;
        LevelController levelController;
        Vector2 center;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.IsFullScreen = false;
            graphics.IsFullScreen = false;

            //fixed time step is a fixed update speed
            //IsFixedTimeStep = true;
            //target elapsed time sets the time between updates 16ms is roughly 60 updates per second
            //this also means roughly 60 frame draws per second
            //TargetElapsedTime = TimeSpan.FromMilliseconds(16);
            graphics.SynchronizeWithVerticalRetrace = true;

            center.X = graphics.PreferredBackBufferWidth / 2;
            center.Y = graphics.PreferredBackBufferHeight / 2;
        }

        //OnActivated and OnDeactivated adjust what the window title is if the game is alt-tabbed
        protected override void OnActivated(object sender, EventArgs args)
        {
            this.Window.Title = "Active Game";
            base.OnActivated(sender, args);
        }
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            this.Window.Title = "InActive Game";
            base.OnActivated(sender, args);
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();

            AudioController.Instance.LoadContent(Content);
            AudioController.Instance.Play();
            menuController = new MenuController(font, center, this);
            levelController = new LevelController(false);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                /*
                if (levelController.levelOpen)
                {
                    levelController.SaveGame();
                }
                */
                Exit();
            }
            //only update menu if there is a menu active
            if (menuController.menuOpen)
            {
                menuController.Update(gameTime, this);
            }
            //only update levelcontroller if there is a level active
            if (levelController.levelOpen)
            {
                levelController.Update(gameTime, Content);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //this clears the leftover pixels and shit. paints them over with black
            GraphicsDevice.Clear(Color.White);
            
            //only draw menu if there is a menu active
            if (menuController.menuOpen)
            {
                menuController.Draw(gameTime, spriteBatch, center);
            }
            //only draw levelcontroller if there is a level active
            if (levelController.levelOpen)
            {
                levelController.Draw(spriteBatch);
            }
            base.Draw(gameTime);
        }

        public void Quit()
        {
            this.Exit();
        }

        public void LoadLevel()
        {
            levelController.LoadLevel(this, font);
        }

        public void LoadMenu()
        {
        }
    }
}
