using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    class MainMenu : Menu
    {
        public MainMenu(SpriteFont font)
        {
            scale = 1.0f;
            Vector2 center = Camera.Instance.cam.Origin;
            itemCount += AddItem("New Game", font, center, (Game1 g, MenuController m) => { g.LoadNewLevel(); m.UnloadContent(); return 0; });
            itemCount += AddItem("Load Game", font, center, (Game1 g, MenuController m) => { g.LoadLevel(); m.UnloadContent(); return 0; });
            itemCount += AddItem("Options", font, center);
            itemCount += AddItem("Exit", font, center, (Game1 g) => { g.Quit(); return 0; });
            SetPositions(center);
        }
    }
}