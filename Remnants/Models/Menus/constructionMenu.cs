using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Remnants
{
    class ConstructionMenu : Menu
    {
        public ConstructionMenu(float scale, SpriteFont font, List<string> stringList, Vector2 center)
        {
            foreach(string s in stringList)
            {
                itemCount += AddItem(scale, s, font, center, () => { return s.Replace(" ", ""); });
            }
            //Align(center, stringList, font, scale);
            SetPositions(center);
        }

        void Align(Vector2 center, List<string> stringList, SpriteFont font, float scale)
        {
            float totHeight = 0f;
            foreach (string st in stringList)
            {
                Vector2 tempVect = (font.MeasureString(st) * scale);
                totHeight += (tempVect.Y);
            }
            float y = center.Y;// - totHeight / 2;
            foreach (MenuItem item in menuItemList)
            {
                item.position = new Vector2(center.X, y);
                y += (font.MeasureString(item.GetText()) * scale).Y + 2;
            }
        }

        public override string Update(MouseState state, MouseState prevState)
        {
            string s = "";
            foreach (MenuItem item in menuItemList)
            {
                s = item.Update(state, prevState);
                if(s != "")
                {
                    return s;
                }
            }
            return s;
        }
    }
}
