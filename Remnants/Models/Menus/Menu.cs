using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Remnants
{
    class Menu
    {
        public List<MenuItem> menuItemList = new List<MenuItem>();
        protected int itemCount { get; set; }
        float menuHeight = 0;

        public Menu()
        {

        }

        protected int AddItem(string text, SpriteFont font, Vector2 position)
        {
            MenuItem item = new MenuItem(text, font, position);
            menuItemList.Add(item);
            menuHeight += item.GetSize().Y;
            return 1;
        }

        protected int AddItem(float scale, string text, SpriteFont font, Vector2 position, System.Func<string> menuItemAction)
        {
            MenuItem item = new MenuItem(scale, text, font, position, menuItemAction);
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
                item.position.Y = top + menuPosition * avgHeight;
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

        public virtual string Update(MouseState state, MouseState prevState)
        {
            return "";
        }
    }
}
