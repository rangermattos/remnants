using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Remnants
{
    class MenuController
    {
        public SpriteFont font;
        public Menu currentMenu;
        public bool menuOpen;
        Menu prevMenu;
        Game1 game;
        List<Menu> ActiveMenus = new List<Menu>();
        private static Texture2D backGround;

        private static MenuController instance;

        private MenuController() { }
        private MenuController(SpriteFont font, ContentManager Content, Game1 game)
        {
            this.font = font;
            this.game = game;
            //currentMenu = MainMenu.Instance;
            //prevMenu = currentMenu;
            //menuOpen = true;
            LoadContent(Content);
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

        public void LoadContent(ContentManager Content)
        {
            backGround = Content.Load<Texture2D>("StarsBasic");
        }

        public void UnloadContent(Menu m)
        {
            ActiveMenus.Remove(m);
            m.isActive = false;
            currentMenu = null;
            menuOpen = false;
        }

        public string Update()
        {
            if (InputManager.Instance.EscPressRelease())
            {
                if (ConstructionMenu.Instance.isActive)
                {
                    UI.Instance.UIItemList[10].active = false;
                    UI.Instance.buildingSelected = "";
                    UnloadContent(ConstructionMenu.Instance);
                }
                if (!MainMenu.Instance.isActive && !ConstructionMenu.Instance.isActive)
                {
                    UI.Instance.isActive = false;
                    SetMenu(MainMenu.Instance);
                    return "";
                }
                if (MainMenu.Instance.isActive && LevelController.Instance.levelOpen)
                {
                    UnloadContent(MainMenu.Instance);
                    UI.Instance.isActive = true;
                }
                else
                {
                    game.Quit();
                }
            }

            if (menuOpen && !MainMenu.Instance.isActive)
            {
            }
            if (menuOpen)
            {
                string s;
                int c = ActiveMenus.Count;
                foreach(Menu m in ActiveMenus)
                {
                    s = m.Update(game, this);
                    if(s != "")
                    {
                        return s;
                    }
                    if (ActiveMenus.Count != c)
                        break;
                }
            }

            return "";
        }

        public void Draw(SpriteBatch spriteBatch, ScalingViewportAdapter viewportAdapter)
        {
            if (menuOpen)
            {
                //spriteBatch.Begin(transformMatrix: Camera.Instance.viewportScale);
                spriteBatch.Begin();
                //spriteBatch.Draw(backGround, new Rectangle(0, 0, 5760, 3240), Color.White);
                if (MainMenu.Instance.isActive)
                {
                    spriteBatch.Draw(backGround, Vector2.Zero, Color.White);
                }
                foreach (Menu m in ActiveMenus)
                {
                    m.Draw(spriteBatch);
                    //spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition() - item.GetOrigin(), item.GetColor() * item.alpha, 0.0f, Vector2.Zero, item.scale, SpriteEffects.None, 0.0f);
                    //spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition(), item.GetColor() * item.alpha, 0.0f, item.GetOrigin(), 1.0f, SpriteEffects.None, 0.0f);
                }
                spriteBatch.End();
            }
        }

        public void SetMenu(Menu m)
        {
            m.isActive = true;
            ActiveMenus.Add(m);
            currentMenu = m;
            menuOpen = true;
            Console.WriteLine("menuOpen: " + menuOpen);
        }
    }
}