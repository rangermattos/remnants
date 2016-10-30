using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    class MainMenu : Menu
    {
        public MainMenu(SpriteFont font, Vector2 center, Game1 game, MenuController mc)
        {
            itemCount += AddItem("New Game", font, center, (Game1 g, MenuController m) => { g.LoadNewLevel(); m.menuOpen = false; return 0; });
            itemCount += AddItem("Load Game", font, center, (Game1 g, MenuController m) => { g.LoadLevel(); m.menuOpen = false; return 0; });
            itemCount += AddItem("Options", font, center);
            itemCount += AddItem("Exit", font, center, (Game1 g) => { g.Quit(); return 0; });
            SetPositions(center);
        }
    }
}