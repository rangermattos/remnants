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
        private LevelController levelController;
        private readonly int virtualWidth = 1920;
        private readonly int virtualHeight = 1080;
        private static Texture2D backGround;
        private ScalingViewportAdapter viewportAdapter;
        Vector2 vpDim;
        Vector2 camDim;
        Vector2 virtualDim;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //Window.AllowUserResizing = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Window.IsBorderless = true;
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
            backGround = Content.Load<Texture2D>("StarsBasic");

            viewportAdapter = new ScalingViewportAdapter(GraphicsDevice, virtualWidth, virtualHeight);
            Camera.Create(viewportAdapter);
            //Camera.Instance.cam.Zoom = 1f;

            AudioController.Instance.LoadContent(Content);
            AudioController.Instance.Play();
            MenuController.Create(font, Content, this);
            levelController = new LevelController(false);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                
                if (levelController.levelOpen)
                {
                    if (UI.Instance.buildingSelected != "")
                        UI.Instance.buildingSelected = "";
                    levelController.SaveGame();
                }
                
                Exit();
            }

            MenuController.Instance.Update();

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

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix());
            //spriteBatch.Draw(backGround, new Rectangle(0, 0, 5760, 3240), Color.White);
            spriteBatch.Draw(backGround, Vector2.Zero, Color.White);
            //spriteBatch.Draw(backGround, new Rectangle(0, 0, 1920, 1080), Color.White);
            spriteBatch.End();

            //spriteBatch.Begin(transformMatrix: Camera.Instance.cam.GetViewMatrix());
            //only draw levelcontroller if there is a level active
            if (levelController.levelOpen)
            {
                levelController.Draw(spriteBatch);
            }
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
            this.Exit();
        }

        public void LoadNewLevel()
        {
            levelController.LoadNewLevel(this, font);
        }

        public void LoadLevel()
        {
            levelController.LoadLevel(this, font, "savegame.sav");
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

        //public GraphicsDevice getGraphicsDevice() { return GraphicsDevice; }
    }
}
