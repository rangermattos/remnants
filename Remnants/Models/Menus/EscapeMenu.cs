using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    class EscapeMenu : Menu
    {
        private static EscapeMenu instance;
        private EscapeMenu() : base()
        {
            scale = 1.0f;
            Vector2 center = Camera.Instance.cam.Origin;
            SpriteFont font = MenuController.Instance.font;
            itemCount += AddItem("Restart Game", font, center, (Game1 g) => { LevelController.Instance.Restart(g.Content); MenuController.Instance.UnloadContent(this); return 0; });
            itemCount += AddItem("Resume", font, center, () => { LevelController.Instance.PauseGame(); MenuController.Instance.UnloadContent(this); return 0; });
            itemCount += AddItem("Save Game", font, center, () => { LevelController.Instance.SaveGame(); return 0; });
            itemCount += AddItem("Exit Game", font, center, (Game1 g) => { g.Quit(); return 0; });
            SetPositions(center);
        }
        public static EscapeMenu Instance
        {
            get
            {
                if (instance == null)
                    instance = new EscapeMenu();

                return instance;
            }
        }
    }
}
