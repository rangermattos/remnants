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
        public bool loading;
        SpriteFont sf;
        string loadString = "Loading";
        float loadTime = 5.0f;
        float elapsedLoadTime = 0.0f;

        private static LevelController instance;
        private LevelController() { }
        public static LevelController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LevelController();
                }
                return instance;
            }
        }

        public void LoadContent(Game1 game)
        {
        }

        public void UnloadContent(Game1 game)
        {
            currentLevel.UnloadContent(game);
            levelOpen = false;
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            if (levelOpen)
            {
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                var keyboardState = Keyboard.GetState();

                if (!loading)
                {
                    // movement
                    if (keyboardState.IsKeyDown(Keys.W))
                        Camera.Instance.cam.Position -= new Vector2(0, 250) * deltaTime;

                    if (keyboardState.IsKeyDown(Keys.S))
                        Camera.Instance.cam.Position += new Vector2(0, 250) * deltaTime;

                    if (keyboardState.IsKeyDown(Keys.A))
                        Camera.Instance.cam.Position -= new Vector2(250, 0) * deltaTime;

                    if (keyboardState.IsKeyDown(Keys.D))
                        Camera.Instance.cam.Position += new Vector2(250, 0) * deltaTime;

                    currentLevel.Update(gameTime, Content, keyboardState);

                    if (Camera.Instance.cam.Position.X < 0)
                        Camera.Instance.cam.Position = new Vector2(0, Camera.Instance.cam.Position.Y);

                    if (Camera.Instance.cam.Position.Y < 0 - (32 * Camera.Instance.viewportScale.Scale.Y))
                        Camera.Instance.cam.Position = new Vector2(Camera.Instance.cam.Position.X, 0 - (32 * Camera.Instance.viewportScale.Scale.Y));
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (loading)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(sf, loadString, Vector2.Zero, Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                spriteBatch.End();
            }
            else
                currentLevel.Draw(spriteBatch);
        }

        public void SaveGame()
        {
            currentLevel.SaveGame();
        }

        public void LoadNewLevel(Game1 game, SpriteFont font)
        {
            LoadContent(game);
            sf = font;
            //send viewport and mapsize to Camera.Instance.cam2d
            currentLevel = new Level(font);
            //set Camera.Instance.cam to center of map
            //Camera.Instance.cam.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y/2);

            currentLevel.LoadContent(game, Camera.Instance.cam.GetViewMatrix(), Camera.Instance.cam.Origin * 2);
            levelOpen = true;
            //loading = true;
        }

        public void LoadLevel(Game1 game, SpriteFont font, string filename)
        {
            LoadContent(game);
            sf = font;
            //send viewport and mapsize to Camera.Instance.cam2d
            currentLevel = new Level(font, filename);
            //set Camera.Instance.cam to center of map
            //Camera.Instance.cam.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y / 2);
            //set zoom

            currentLevel.LoadContent(game, Camera.Instance.cam.GetViewMatrix(), Camera.Instance.cam.Origin * 2);
            levelOpen = true;
            loading = true;
        }
    }
}