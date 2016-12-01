using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    class MainMenu : Menu
    {
        private static MainMenu instance;
        private MainMenu() : base()
        {
            scale = 1.0f;
            Vector2 center = Camera.Instance.cam.Origin;
            SpriteFont font = MenuController.Instance.font;
            itemCount += AddItem("New Game", font, center, 
                (Game1 g, MenuController m) => {
                    if (Settings.Instance.isActive)
                    {
                        MenuController.Instance.UnloadContent(Settings.Instance);
                    }
                    g.LoadNewLevel();
                    m.UnloadContent(this);
                    return 0; });
            itemCount += AddItem("Load Game", font, center, 
                (Game1 g, MenuController m) => {
                    g.LoadLevel();
                    m.UnloadContent(this);
                    return 0; });
            itemCount += AddItem("Options", font, center, 
                () => {
                    //MenuController.Instance.UnloadContent(this);
                    MenuController.Instance.SetMenu(Settings.Instance);
                    return 1;}) ;
            itemCount += AddItem("Exit", font, center, 
                (Game1 g) => {
                    g.Quit();
                    return 0; });
            SetPositions(center);
        }
        public static MainMenu Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainMenu();

                return instance;
            }
        }
    }
}