﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
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

            currentLevel.Update(gameTime, graphics);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Matrix viewMatrix = camera.GetViewMatrix();
            currentLevel.Draw(spriteBatch, viewMatrix);
        }

        public void LoadLevel(Game1 game, SpriteFont font)
        {
            //send viewport and mapsize to camera2d
            currentLevel = new Level(font);
            currentLevel.LoadContent(game);
            camera = new Camera2D(game.GraphicsDevice.Viewport, currentLevel.mapSize);
            levelOpen = true;
        }
    }
}