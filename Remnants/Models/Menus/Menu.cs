﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Remnants
{
    class Menu
    {
        public List<MenuItem> menuItemList = new List<MenuItem>();
        protected int itemCount { get; set; }
        float menuHeight = 0;
        public float scale;
        public bool isActive;

        public Menu()
        {
            isActive = false;
        }

        protected int AddItem(string text, SpriteFont font, Vector2 position)
        {
            MenuItem item = new MenuItem(text, font, position);
            menuItemList.Add(item);
            menuHeight += item.GetSize().Y;
            return 1;
        }

        protected int AddItem(Building b, float totHeight, float scale, string text, SpriteFont font, Vector2 position, System.Func<string> menuItemAction)
        {
            MenuItem item = new MenuItem(b, totHeight, scale, text, font, position, menuItemAction);
            menuItemList.Add(item);
            menuHeight += item.GetSize().Y;
            return 1;
        }

        protected int AddItem(string text, SpriteFont font, Vector2 position, System.Func<Game1, int> menuItemAction)
        {
            MenuItem item = new MenuItem(text, font, position, menuItemAction);
            menuItemList.Add(item);
            menuHeight += item.GetSize().Y;
            return 1;
        }

        protected int AddItem(string text, SpriteFont font, Vector2 position, System.Func<int> menuItemAction)
        {
            MenuItem item = new MenuItem(text, font, position, menuItemAction);
            menuItemList.Add(item);
            menuHeight += item.GetSize().Y;
            return 1;
        }
        /*
        protected int AddItem(float scale, string text, SpriteFont font, Vector2 position, System.Func<int> menuItemAction)
        {
            MenuItem item = new MenuItem(scale, text, font, position, menuItemAction);
            menuItemList.Add(item);
            menuHeight += item.GetSize().Y;
            return 1;
        }
        /*/

        protected int AddItem(string text, SpriteFont font, Vector2 position, System.Func<Game1, MenuController, int> menuItemAction)
        {
            MenuItem item = new MenuItem(text, font, position, menuItemAction);
            menuItemList.Add(item);
            menuHeight += item.GetSize().Y;
            return 1;
        }

        protected void SetPositions(Vector2 center)
        {
            float avgHeight = menuHeight / itemCount;
            float top = SetTop(avgHeight, center);

            int menuPosition = 0;
            foreach (MenuItem item in menuItemList)
            {
                item.position.Y = top + menuPosition * avgHeight;
                item.position = Vector2.Transform(item.position, Camera.Instance.viewportScale);
                //item.origin = Vector2.Transform(item.origin, Camera.Instance.viewportScale);
                menuPosition++;
            }
        }

        protected void SetPositions(Vector2 center, int x)
        {
            float avgHeight = menuHeight / itemCount;
            float top = center.Y - (menuHeight / 2);

            int menuPosition = 0;
            foreach (MenuItem item in menuItemList)
            {
                item.origin.X = 0;
                item.origin.Y = 0;
                item.position.X = 2;//center.X - item.GetSize().X / 2;// - item.origin.X;
                item.position.Y = top + menuPosition * avgHeight;
                item.position = Vector2.Transform(item.position, Camera.Instance.viewportScale);
                item.origin = Vector2.Transform(item.origin, Camera.Instance.viewportScale);
                menuPosition++;
            }
        }

        float SetTop(float avgHeight, Vector2 center)
        {
            if (itemCount % 2 == 0)
            {
				return center.Y - ((itemCount) / 2 ) * avgHeight + 0.5f * avgHeight;
            }
            else
            {
                return center.Y - ((itemCount - 1) / 2) * avgHeight;
            }
        }

        public List<MenuItem> GetMenu()
        {
            return menuItemList;
        }

        public virtual void Update(Game1 game)
        {
            for (int i = 0; i < 3; i++)
            {
                menuItemList[i].color = Color.White;
            }
            menuItemList[LevelData.Instance.difficulty - 1].color = Color.Green;
            foreach (MenuItem item in menuItemList)
            {
                item.Update(game);
            }
        }

        public virtual string Update(Game1 game, MenuController mc)
        {
            string s = "";
            foreach (MenuItem item in menuItemList)
            {
                s = item.Update(game, mc);
                if (s != "")
                {
                    UI.Instance.buildingSelected = s;
                    Console.WriteLine("buildingSelected: " + UI.Instance.buildingSelected);
					UI.Instance.EnqueueMessage("Building selected: " + UI.Instance.buildingSelected);
                    return s;
                }
            }
            return s;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem item in menuItemList)
            {
                item.Draw(spriteBatch);
                //spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition() - item.GetOrigin(), item.GetColor() * item.alpha, 0.0f, Vector2.Zero, item.scale, SpriteEffects.None, 0.0f);
                //spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition(), item.GetColor() * item.alpha, 0.0f, item.GetOrigin(), 1.0f, SpriteEffects.None, 0.0f);
            }
        }
    }
}
