using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Remnants
{
    class Level
    {
        Texture2D backGround;
        protected Map map;
        public Vector2 mapSize;
        List<Building> buildings = new List<Building>();
        KeyboardState prevKeyState;
        MouseState prevMouseState;
        MouseState mouseState;
        Vector2 mousePosition;

        public Level(SpriteFont font)
        {
            mapSize = new Vector2(5760, 3240);
            map = new Map(mapSize);
        }

        public void LoadContent(Game1 game)
        {
            backGround = game.Content.Load<Texture2D>("StarsBasic");
            map.LoadContent(game.Content);
        }

        public void UnloadContent(Game1 game)
        {
            game.Content.Unload();
        }

        public void Update(GameTime gameTime, ContentManager Content, KeyboardState keyboardState)
        {
            mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);
            if (prevKeyState.IsKeyDown(Keys.Space) && keyboardState.IsKeyUp(Keys.Space))
            {
                buildings.Add(new SolarPanel(Content, mousePosition));
            }



            prevKeyState = keyboardState;
            prevMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch, Camera2D camera)
        {
            var viewMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);
            spriteBatch.Draw(backGround, new Rectangle(0, 0, 5760, 3240), Color.White);
            map.Draw(spriteBatch, camera);
            foreach (Building b in buildings)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.End();

            //map.Draw(graphics);

            //ui.Draw(spriteBatch);
        }
    }
}
