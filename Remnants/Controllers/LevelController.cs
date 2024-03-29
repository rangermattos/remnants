﻿using Microsoft.Xna.Framework;
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
        float loadTime = 6.0f;
        float elapsedLoadTime = 0.0f;
        SpriteFont font;
        public int diff = 0;

        private static LevelController instance;
        private LevelController()
        {
            levelOpen = false;
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

                if (!loading)
                {
                    currentLevel.Update(gameTime, Content);
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
                            loadString = "Loading";
                            loadTime = 5f;
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
                    spriteBatch.DrawString(font, loadString, Vector2.Zero, Color.WhiteSmoke, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
					spriteBatch.DrawString(font, "Use WASD to move the camera", new Vector2(0,75), Color.WhiteSmoke, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "Click the hammer icon in the bottom left to construct buildings", new Vector2(0,125), Color.WhiteSmoke, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "Right click to exit build mode", new Vector2(0,175), Color.WhiteSmoke, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "Disable and enable buildings with left click while not in build mode", new Vector2(0,225), Color.WhiteSmoke, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
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

        public void PauseGame()
        {
            if(!(currentLevel.paused && !EscapeMenu.Instance.isActive))
                currentLevel.paused = !currentLevel.paused;
        }

        public void LoadNewLevel(ContentManager Content)
        {
            if (MainMenu.Instance.isActive)
            {
                MenuController.Instance.UnloadContent(MainMenu.Instance);
            }
            if (Settings.Instance.isActive)
            {
                MenuController.Instance.UnloadContent(Settings.Instance);
            }
            diff += LevelData.Instance.difficulty;
            //send viewport and mapsize to Camera.Instance.cam2d
            currentLevel = new Level();
            //set Camera.Instance.cam to center of map
            //Camera.Instance.cam.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y/2);

            currentLevel.LoadContent(Content);
            levelOpen = true;
            loading = true;
            UI.Instance.isActive = true;
        }

        public void LoadLevel(ContentManager Content, string filename)
        {
            if (MainMenu.Instance.isActive)
            {
                MenuController.Instance.UnloadContent(MainMenu.Instance);
            }
            if (Settings.Instance.isActive)
            {
                MenuController.Instance.UnloadContent(Settings.Instance);
            }
            //send viewport and mapsize to Camera.Instance.cam2d
            currentLevel = new Level(filename);
            //set Camera.Instance.cam to center of map
            //Camera.Instance.cam.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y / 2);
            //set zoom

            currentLevel.LoadContent(Content);
            levelOpen = true;
            loading = true;
            UI.Instance.isActive = true;
        }

        public void Restart(ContentManager Content)
        {
            LevelData.Instance.Reset();
            levelOpen = false;
            currentLevel = new Level(diff);
            //set Camera.Instance.cam to center of map
            //Camera.Instance.cam.Position = new Vector2(currentLevel.mapSize.X / 2, currentLevel.mapSize.Y/2);

            currentLevel.LoadContent(Content);
            levelOpen = true;
            loading = true;
            UI.Instance.isActive = true;
        }
    }
}