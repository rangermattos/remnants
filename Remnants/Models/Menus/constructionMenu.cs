using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Remnants
{
    class ConstructionMenu : Menu
    {
        public ConstructionMenu(float scale, SpriteFont font, List<string> stringList, Vector2 center)
        {
            foreach(string s in stringList)
            {
                itemCount += AddItem(s, font, center);
            }
            SetPositions(center);
        }
    }
}
