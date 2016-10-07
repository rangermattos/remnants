using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Remnants
{
    class MenuController
    {
        Menu currentMenu;
        Texture2D backGround;
        public bool menuOpen;
        public MenuController(SpriteFont font, Vector2 center, Game1 game)
        {
            currentMenu = new MainMenu(font, center, game, this);
            menuOpen = true;
            backGround = game.Content.Load<Texture2D>("StarsBasic");
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
            currentMenu.menuItemList.Clear();
            menuOpen = false;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            MouseState state = Mouse.GetState();
            foreach (MenuItem item in currentMenu.menuItemList)
            {
                item.Update(state, game, this);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 center)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Rectangle(0, 0, 5760, 3240), Color.White);
            foreach (MenuItem item in currentMenu.menuItemList)
            {
                spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition(), item.GetColor() * item.alpha, 0.0f, item.GetOrigin(), 1.0f, SpriteEffects.None, 0.0f);
            }
            spriteBatch.End();
        }
    }
}