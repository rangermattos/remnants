using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Remnants
{
    class UIItem
    {
        string value;
        Texture2D texture;
        Vector2 size = new Vector2();
        public Vector2 topLeft { get; set; }
        SpriteFont font;
        Color color;
        public Vector2 position { get; set; }
        public float alpha { get; set; }
        public int sign = -1;
        System.Func<int> Action1;
        //System.Func<Game1, int> Action1;
        System.Func<Game1, MenuController, int> Action2;

        public UIItem(SpriteFont incfont, Vector2 tl, Vector2 incposition, Texture2D txt, System.Func<int> UIItemAction)
        {
            font = incfont;
            position = incposition;
            topLeft = tl;
            texture = txt;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position + topLeft);
        }
    }
}
