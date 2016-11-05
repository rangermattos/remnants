using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
    class MenuController
    {
        public SpriteFont font;
        public Menu currentMenu;
        Menu prevMenu;
        //public bool menuOpen { get; set; }
        public bool menuOpen;
        Game1 game;

        private static MenuController instance;

        private MenuController() { }
        private MenuController(SpriteFont font, ContentManager content, Game1 game)
        {
            this.font = font;
            this.game = game;
            currentMenu = new MainMenu(font);
            prevMenu = currentMenu;
            menuOpen = true;
        }

        public static MenuController Instance
        {
            get
            {
                if(instance == null)
                {
                    throw new Exception("Object not created");
                }
                return instance;
            }
        }

        public static void Create(SpriteFont font, ContentManager content, Game1 game)
        {
            if (instance != null)
            {
                throw new Exception("Object already created");
            }
            instance = new MenuController(font, content, game);
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
            currentMenu = null;
            menuOpen = false;
            Console.WriteLine("menuOpen: " + menuOpen);
        }

        public string Update()
        {
            if (menuOpen)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    UI.Instance.UIItemList[10].active = false;
                    UI.Instance.buildingSelected = "";
                    UnloadContent();
                    
                    return "";
                }
                MouseState state = Mouse.GetState();
                return currentMenu.Update(state, game, this);
            }
            else
            {
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                UI.Instance.buildingSelected = "";
                //SetMenu(new MainMenu(font));
                game.Quit();
                
            }

            return "";
        }

        public void Draw(SpriteBatch spriteBatch, ScalingViewportAdapter viewportAdapter)
        {
            if (menuOpen)
            {
                spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix());
                foreach (MenuItem item in currentMenu.menuItemList)
                {
                    item.Draw(spriteBatch);
                    //spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition() - item.GetOrigin(), item.GetColor() * item.alpha, 0.0f, Vector2.Zero, item.scale, SpriteEffects.None, 0.0f);
                    //spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition(), item.GetColor() * item.alpha, 0.0f, item.GetOrigin(), 1.0f, SpriteEffects.None, 0.0f);
                }
                spriteBatch.End();
            }
        }

        public void SetMenu(Menu m)
        {
            currentMenu = m;
            menuOpen = true;
            Console.WriteLine("menuOpen: " + menuOpen);
        }
    }
}