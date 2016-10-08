using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Remnants
{
    class Level
    {
        UI ui;
        Texture2D backGround;
        protected Map map;
        public Vector2 mapSize;
        List<Building> buildings = new List<Building>();
        KeyboardState prevKeyState;
        MouseState prevMouseState;
        MouseState mouseState;
        Vector2 mousePosition;
        string mp;
        SpriteFont font;
        int x = 0;
        int y = 0;
        int power;
        int metal;

        public Level(SpriteFont f)
        {
            font = f;
            mapSize = new Vector2(5760, 3240);
            map = new Map(mapSize);
            power = 1000;
            metal = 1000;
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

        public void Update(GameTime gameTime, ContentManager Content, KeyboardState keyboardState, Camera2D camera)
        {
            mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);
            if (prevKeyState.IsKeyDown(Keys.Space) && keyboardState.IsKeyUp(Keys.Space))
            {
                //gets the mouses position in the world and sets it in p
                Vector2 p = Vector2.Transform(mousePosition, Matrix.Invert(camera.GetViewMatrix()));
                //get the tile x and y position
                int x = (int)Math.Floor(p.X / 64);
                int y = (int)Math.Floor(p.Y / 64);
                //scale back up, p will now be in line with the tiles
                p = new Vector2(x*64f, y*64);
                buildings.Add(new SolarPanel(Content, p));
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
            spriteBatch.DrawString(font, x.ToString(), new Vector2(camera.Position.X, camera.Position.Y), Color.Black);
            spriteBatch.End();

            //map.Draw(graphics);

            //ui.Draw(spriteBatch);
        }
    }
}
