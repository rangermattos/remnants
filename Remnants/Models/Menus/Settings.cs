using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    class Settings : Menu
    {
        private static Settings instance;
        private Settings() : base()
        {
            scale = 1.0f;
            Vector2 center = Camera.Instance.cam.Origin;
            SpriteFont font = MenuController.Instance.font;
            itemCount += AddItem("Easy", font, center,
                () => {
                    LevelData.Instance.difficulty = 1;
                    return 0;
                });
            itemCount += AddItem("Medium", font, center,
                () => {
                    LevelData.Instance.difficulty = 2;
                    return 0;
                });
            itemCount += AddItem("Hard", font, center,
                () => {
                    LevelData.Instance.difficulty = 3;
                    return 0;
                });
            itemCount += AddItem("Return", font, center, 
                () => {
                    MenuController.Instance.SetMenu(MainMenu.Instance);
                    return 0; }) ;
            SetPositions(center);
        }
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                    instance = new Settings();

                return instance;
            }
        }
    }
}