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
        public ContentManager cont;

        private static MenuController instance;

        private MenuController() { }
        private MenuController(SpriteFont font, ContentManager Content, Game1 game)
        {
            this.font = font;
            this.game = game;
            cont = Content;
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
            currentMenu = prevMenu;
            menuOpen = false;
        }

        public string Update()
        {
            if (LevelController.Instance.levelOpen) {
                if (InputManager.Instance.EscPressRelease())
                {
                    if (ConstructionMenu.Instance.isActive)
                    {
                        UI.Instance.UIItemList[9].active = false;
                        UI.Instance.buildingSelected = "";
                        UnloadContent(ConstructionMenu.Instance);
                        return "";
                    }
                    if (!EscapeMenu.Instance.isActive && !ConstructionMenu.Instance.isActive)
                    {
                        LevelController.Instance.PauseGame();
                        SetMenu(EscapeMenu.Instance);
                        return "";
                    }
                    if (EscapeMenu.Instance.isActive && LevelController.Instance.levelOpen)
                    {
                        LevelController.Instance.PauseGame();
                        UnloadContent(EscapeMenu.Instance);
                        return "";
                    }
                    else
                    {
                        game.Quit();
                    }
                }
                else if(InputManager.Instance.EscPressRelease())
                {
                    game.Quit();
                }

                if (menuOpen)
                {
                    string s;
                    int c = ActiveMenus.Count;
                        foreach (Menu m in ActiveMenus)
                        {
                            s = m.Update(game, this);
                            if (s != "")
                            {
                                return s;
                            }
                            if (ActiveMenus.Count != c)
                                break;
                        }
                    
                }
            }
            else
            {
                if (currentMenu is MainMenu)
                    currentMenu.Update(game, this);
                if(currentMenu is Settings)
                    currentMenu.Update(game);
            }
            return "";
        }

        public void Draw(SpriteBatch spriteBatch, ScalingViewportAdapter viewportAdapter)
        {
            if (menuOpen)
            {
                spriteBatch.Begin();
                if (!LevelController.Instance.levelOpen)
                {
                    spriteBatch.Draw(backGround, Vector2.Zero, Color.White);
                    currentMenu.Draw(spriteBatch);
                }
                else
                {
                    //spriteBatch.Begin(transformMatrix: Camera.Instance.viewportScale);
                    //spriteBatch.Draw(backGround, new Rectangle(0, 0, 5760, 3240), Color.White);
                    /*/
                    if (MainMenu.Instance.isActive)
                    {
                    }
                    if (Settings.Instance.isActive)
                    {
                        spriteBatch.Draw(backGround, Vector2.Zero, Color.White);
                        Settings.Instance.Draw(spriteBatch);
                    }
                    /*/
                    //else
                    //{
                    foreach (Menu m in ActiveMenus)
                    {
                        if (!(m is MainMenu || m is Settings))
                        {
                            m.Draw(spriteBatch);
                        }
                        //spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition() - item.GetOrigin(), item.GetColor() * item.alpha, 0.0f, Vector2.Zero, item.scale, SpriteEffects.None, 0.0f);
                        //spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition(), item.GetColor() * item.alpha, 0.0f, item.GetOrigin(), 1.0f, SpriteEffects.None, 0.0f);
                    }
                    //}
                }
                spriteBatch.End();
            }
        }

        public void SetMenu(Menu m)
        {
            m.isActive = true;
            ActiveMenus.Add(m);
            prevMenu = null;
            if (m is Settings && currentMenu is MainMenu)
            {
                prevMenu = currentMenu;
                //Settings.Instance.isActive = false;
                Console.WriteLine("setting prevMenu to " + prevMenu.GetType().ToString());
            }
            currentMenu = m;
            menuOpen = true;
        }
    }
}