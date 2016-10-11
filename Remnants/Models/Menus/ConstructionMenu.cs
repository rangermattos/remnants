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
            SetPositions(center);
            //Alignx(center, stringList, font, scale);
        }

        void Alignx(Vector2 center, List<string> stringList, SpriteFont font, float scale)
        {
            float maxWidth = 0f;
            foreach (string st in stringList)
            {
                Vector2 tempVect = (font.MeasureString(st) * scale);
                if(tempVect.X > maxWidth)
                {
                    maxWidth = tempVect.X;
                }
            }
            foreach (MenuItem item in menuItemList)
            {
                item.position = new Vector2(10, item.position.Y);
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
