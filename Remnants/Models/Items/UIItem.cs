using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Remnants
{
    class UIItem
    {
        Texture2D texture;
        Vector2 size = new Vector2();
        public Vector2 topLeft { get; set; }
        public Vector2 position { get; set; }
        public Vector2 valuePosition { get; set; }
        public int value { get; set; }
        SpriteFont font;
        Color color;
        public float alpha { get; set; }
        public float scale { get; set; }
        public int sign = -1;
        System.Func<int> Action1;
        //System.Func<Game1, int> Action1;
        System.Func<Game1, MenuController, int> Action2;

        public UIItem(Vector2 tl, Vector2 incposition, Texture2D txt, System.Func<int> UIItemAction)
        {
            position = incposition;
            topLeft = tl;
            texture = txt;
        }

        public UIItem(float s, int v, SpriteFont incfont, Vector2 tl, Vector2 incposition, Texture2D txt, System.Func<int> UIItemAction)
        {
            scale = s;
            value = v;
            font = incfont;

            size = (font.MeasureString(value.ToString())) * scale;
            valuePosition = new Vector2(0, (32 - size.Y) / 2);

            position = incposition;
            topLeft = tl;
            texture = txt;

            color = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position + topLeft);
            if (font != null)
            {
                spriteBatch.DrawString(font, value.ToString(), valuePosition, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
            }
        }
    }
}
