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
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private readonly int virtualWidth = 1920;
        private readonly int virtualHeight = 1080;
        //private static Texture2D backGround;
        private ScalingViewportAdapter viewportAdapter;
        bool gameActive;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //Window.AllowUserResizing = true;
            //graphics.PreferredBackBufferWidth = 1366;
            //graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Window.IsBorderless = true;
            gameActive = true;
            //graphics.IsFullScreen = true;

            /*
            //fixed time step is a fixed update speed
            //IsFixedTimeStep = true;
            //target elapsed time sets the time between updates 16ms is roughly 60 updates per second
            //this also means roughly 60 frame draws per second
            //TargetElapsedTime = TimeSpan.FromMilliseconds(16);
            graphics.SynchronizeWithVerticalRetrace = true;

            center.X = graphics.PreferredBackBufferWidth / 2;
            center.Y = graphics.PreferredBackBufferHeight / 2;
            */
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            //backGround = Content.Load<Texture2D>("StarsBasic");

            viewportAdapter = new ScalingViewportAdapter(GraphicsDevice, virtualWidth, virtualHeight);
            Camera.Create(viewportAdapter);
            //Camera.Instance.cam.Zoom = 1f;
            AudioController.Instance.LoadContent(Content);
            AudioController.Instance.Play();
            MenuController.Create(font, Content, this);
            MenuController.Instance.SetMenu(MainMenu.Instance);
            //UI.Create(Content);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameActive)
            {
                InputManager.Instance.Update();
				Camera.Instance.Update(gameTime);
				LevelController.Instance.Update(gameTime, Content);
                MenuController.Instance.Update();
				DevCheats.Instance.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //this clears the leftover pixels and shit. paints them over with black
            GraphicsDevice.Clear(Color.Black);

            //spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix());
            //spriteBatch.End();

            //spriteBatch.Begin(transformMatrix: Camera.Instance.cam.GetViewMatrix());
            //only draw levelcontroller if there is a level active
            LevelController.Instance.Draw(spriteBatch);
            MenuController.Instance.Draw(spriteBatch, viewportAdapter);
            /*
            spriteBatch.Begin();
            spriteBatch.DrawString(font, Camera.Instance.cam.Position.ToString(), new Vector2(0f, 50f), Color.WhiteSmoke, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, Camera.Instance.cam.Origin.ToString(), new Vector2(0f, 100f), Color.WhiteSmoke, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, Camera.Instance.cam.ScreenToWorld(Vector2.Zero).ToString(), new Vector2(0f, 150f), Color.WhiteSmoke, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
            spriteBatch.End();
            */
            base.Draw(gameTime);
        }

        public void Quit()
        {
            //UnloadContent();
            this.Exit();
        }

        public void LoadNewLevel()
        {
            LevelController.Instance.LoadNewLevel(Content);
        }

        public void LoadLevel()
        {
            LevelController.Instance.LoadLevel(Content, "savegame.sav");
        }

        //OnActivated and OnDeactivated adjust what the window title is if the game is alt-tabbed
        protected override void OnActivated(object sender, EventArgs args)
        {
            this.Window.Title = "Active Game";
            gameActive = true;
            base.OnActivated(sender, args);
        }
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            this.Window.Title = "InActive Game";
            gameActive = false;
            base.OnActivated(sender, args);
        }

        //public GraphicsDevice getGraphicsDevice() { return GraphicsDevice; }
    }
}
