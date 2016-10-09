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
                //itemCount += AddItem(s, font, center);
                itemCount += AddItem(s, font, center, () => { return s.Replace(" ", ""); });
            }
            SetPositions(center);
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
