using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Remnants
{
    class UI
    {
        public List<UIItem> UIItemList = new List<UIItem>();
        Vector2 topLeft = new Vector2();

        public UI(SpriteFont font, Vector2 position, ContentManager Content, Vector2 viewport)
        {
            topLeft = position;
            position.X = 0;
            position.Y = ((viewport.Y) - 32);
            AddItem(font, topLeft, position, Content.Load<Texture2D>("construct"), () => { OnClickConstruct(); return 0; });
        }

        public void Update(GameTime gameTime, Vector2 tl)
        {
            foreach (UIItem i in UIItemList)
            {
                i.topLeft = tl;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (UIItem i in UIItemList)
            {
                i.Draw(spriteBatch);
            }
        }

        void AddItem(SpriteFont font, Vector2 tl, Vector2 position, Texture2D texture, System.Func<int> UIItemAction)
        {
            UIItem item = new UIItem(font, tl, position, texture, UIItemAction);
            UIItemList.Add(item);
        }

        void OnClickConstruct()
        {

        }
    }
}
