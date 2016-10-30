using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    class LevelController
    {
        Level currentLevel;
        public bool levelOpen;
        protected Camera2D camera;
        public bool loading;
        SpriteFont sf;
        string loadString = "Loading";
        float loadTime = 5.0f;
        float elapsedLoadTime = 0.0f;
        Texture2D backGround;

        public LevelController(bool isOpen)
        {
            levelOpen = isOpen;
        }

        public void LoadContent(Game1 game)
        {
            backGround = game.Content.Load<Texture2D>("StarsBasic");
        }

        public void UnloadContent(Game1 game)
        {
            currentLevel.UnloadContent(game);
            levelOpen = false;
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            if (!loading)
            {
                // movement
                if (keyboardState.IsKeyDown(Keys.W))
                    camera.Position -= new Vector2(0, 250) * deltaTime;

                if (keyboardState.IsKeyDown(Keys.S))
                    camera.Position += new Vector2(0, 250) * deltaTime;

                if (keyboardState.IsKeyDown(Keys.A))
                    camera.Position -= new Vector2(250, 0) * deltaTime;

                if (keyboardState.IsKeyDown(Keys.D))
                    camera.Position += new Vector2(250, 0) * deltaTime;

                currentLevel.Update(gameTime, Content, keyboardState, camera);
            }
            else
            {
                elapsedLoadTime += deltaTime;
                if (elapsedLoadTime >= 1.0f)
                {
                    loadString += ".";
                    loadTime -= 1f;
                    elapsedLoadTime = 0f;
                    if (loadTime == 0f)
                    {
                        loading = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (loading)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backGround, new Rectangle(0, 0, 5760, 3240), Color.White);
                spriteBatch.DrawString(sf, loadString, Vector2.Zero, Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                spriteBatch.End();
            }
            else
                currentLevel.Draw(spriteBatch, camera);
        }

        public void SaveGame()
        {
            currentLevel.SaveGame();
        }

        public void LoadNewLevel(Game1 game, SpriteFont font)
        {
            LoadContent(game);
            sf = font;
            //send viewport and mapsize to camera2d
            currentLevel = new Level(font);
            camera = new Camera2D(game.GraphicsDevice.Viewport, currentLevel.mapSize);
            //set camera to center of map
            camera.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y/2);
            //set zoom
            camera.Zoom = 0.8f;
            //camera.Zoom = 1f;
            currentLevel.LoadContent(game, camera.GetViewMatrix(), camera.Origin * 2);
            levelOpen = true;
            loading = true;
        }

        public void LoadLevel(Game1 game, SpriteFont font, string filename)
        {
            LoadContent(game);
            sf = font;
            //send viewport and mapsize to camera2d
            currentLevel = new Level(font, filename);
            camera = new Camera2D(game.GraphicsDevice.Viewport, currentLevel.mapSize);
            //set camera to center of map
            camera.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y / 2);
            //set zoom
            camera.Zoom = 0.8f;
            //camera.Zoom = 1f;
            currentLevel.LoadContent(game, camera.GetViewMatrix(), camera.Origin * 2);
            levelOpen = true;
            loading = true;
        }
    }
}