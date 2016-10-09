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

        int energy;
        int metal;
        int pop;
        int food;
        int water;
        int antimatter;
        int wood;
        int nuclear;
        List<int> resourceList = new List<int>();

        public Level(SpriteFont f)
        {
            font = f;
            mapSize = new Vector2(5760, 3240);
            map = new Map(mapSize);
            food = 10000;
            resourceList.Add(food);
            water = 10000;
            resourceList.Add(water);
            energy = 10000;
            resourceList.Add(energy);
            antimatter = 10000;
            resourceList.Add(antimatter);
            nuclear = 10000;
            resourceList.Add(nuclear);
            wood = 10000;
            resourceList.Add(wood);
            metal = 10000;
            resourceList.Add(metal);
            pop = 10000;
            resourceList.Add(pop);
        }

        public void LoadContent(Game1 game, Matrix vm, Vector2 vpDimensions)
        {
            backGround = game.Content.Load<Texture2D>("StarsBasic");
            map.LoadContent(game.Content);
            ui = new UI(font, Vector2.Transform(Vector2.Zero, Matrix.Invert(vm)), game.Content, vpDimensions, resourceList);
        }

        public void UnloadContent(Game1 game)
        {
            game.Content.Unload();
        }

        public void Update(GameTime gameTime, ContentManager Content, KeyboardState keyboardState, Camera2D camera)
        {
            mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);
            var deltaT = gameTime.ElapsedGameTime.TotalSeconds;
            var vm = camera.GetViewMatrix();
            if (prevKeyState.IsKeyDown(Keys.Space) && keyboardState.IsKeyUp(Keys.Space))
            {
                //gets the mouses position in the world and sets it in p
                Vector2 p = Vector2.Transform(mousePosition, Matrix.Invert(vm));
                //get the tile x and y position
                int x = (int)Math.Floor(p.X / 64);
                int y = (int)Math.Floor(p.Y / 64);
                //scale back up, p will now be in line with the tiles
                p = new Vector2(x*64f, y*64);
                buildings.Add(new SolarPanel(Content, p));
            }

            foreach(Building b in buildings)
            {
                b.Update(gameTime);
                if (b.energyChange != 0)
                {
                    energy += b.energyChange;
                    b.energyChange = 0;
                }
                //if (b.GetType().Name == "SolarPanel") ;
            }

            ui.Update(gameTime, mouseState, prevMouseState, resourceList);
            
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

            spriteBatch.Begin();
            ui.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
