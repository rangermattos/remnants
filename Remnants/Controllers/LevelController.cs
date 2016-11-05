using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
	enum resources { FOOD, WATER, ENERGY, NUCLEAR, ANTIMATTER, WOOD, METAL, POP };

    class LevelController
    {
        Level currentLevel;
        public bool levelOpen;
        public bool loading;
        string loadString = "Loading";
        float loadTime = 5.0f;
        float elapsedLoadTime = 0.0f;
        SpriteFont font;

        private static LevelController instance;
        private LevelController()
        {
            font = MenuController.Instance.font;
        }
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

        public void LoadContent(ContentManager Content) { }

        public void UnloadContent(ContentManager Content)
        {
            levelOpen = false;
            currentLevel.UnloadContent(Content);
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

                    currentLevel.Update(gameTime, Content);

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
                            UI.Instance.isActive = true;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (levelOpen)
            {
                if (loading)
                {
                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, loadString, Vector2.Zero, Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    spriteBatch.End();
                }
                else
                    currentLevel.Draw(spriteBatch);
            }
        }

        public void SaveGame()
        {
            currentLevel.SaveGame();
        }

        public void LoadNewLevel(ContentManager Content)
        {
            LoadContent(Content);
            //send viewport and mapsize to Camera.Instance.cam2d
            currentLevel = new Level();
            //set Camera.Instance.cam to center of map
            //Camera.Instance.cam.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y/2);

            currentLevel.LoadContent(Content);
            levelOpen = true;
            loading = true;
        }

        public void LoadLevel(ContentManager Content, string filename)
        {
            LoadContent(Content);
            //send viewport and mapsize to Camera.Instance.cam2d
            currentLevel = new Level(filename);
            //set Camera.Instance.cam to center of map
            //Camera.Instance.cam.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y / 2);
            //set zoom

            currentLevel.LoadContent(Content);
            levelOpen = true;
            loading = true;
        }
    }
}