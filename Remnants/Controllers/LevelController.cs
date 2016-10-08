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
        protected UI ui;

        public LevelController(bool isOpen)
        {
            levelOpen = isOpen;
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
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

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

        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch, camera);
        }

        public void LoadLevel(Game1 game, SpriteFont font)
        {
            //send viewport and mapsize to camera2d
            currentLevel = new Level(font);
            currentLevel.LoadContent(game);
            camera = new Camera2D(game.GraphicsDevice.Viewport, currentLevel.mapSize);
            //set camera to center of map
            camera.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y/2);
            //set zoom
            camera.Zoom = 0.8f;
            //camera.Zoom = 1f;
            levelOpen = true;
        }
    }
}