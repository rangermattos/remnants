using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace Remnants
{
    class UIItem
    {
        Texture2D texture;
        Vector2 size = new Vector2();
        public Vector2 topLeft { get; set; }
        public Vector2 position { get; set; }
        public Vector2 valuePosition { get; set; }
        Vector2 textureScale;
        public int value { get; set; }
        SpriteFont font;
        Color color;
        public float alpha { get; set; }
        public float scale { get; set; }
        public int sign = -1;
        System.Func<int> Action1;
        //System.Func<Game1, int> Action1;
        System.Func<bool, int> Action2;
        public bool active { get; set; }
        Menu popUpMenu;

        public UIItem(Vector2 tl, Vector2 incposition, Texture2D txt, System.Func<int> UIItemAction)
        {
            position = incposition;
            topLeft = tl;
            texture = txt;
            textureScale = new Vector2(1f, 1f);
            color = Color.White;
            active = false;
            size = new Vector2((float)texture.Width, (float)texture.Height);
        }
        public UIItem(Vector2 tl, Vector2 incposition, Texture2D txt, System.Func<bool, int> UIItemAction)
        {
            position = incposition;
            topLeft = tl;
            texture = txt;
            textureScale = new Vector2(1f, 1f);
            color = Color.White;

            Action2 = UIItemAction;
            active = false;
            size = new Vector2((float)texture.Width, (float)texture.Height);
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
            textureScale = new Vector2(1f, 1f);

            color = Color.White;
            active = false;
            size = new Vector2((float)texture.Width, (float)texture.Height);
        }
        /*
        public UIItem(float s, List<string> stringList, SpriteFont incfont, Vector2 tl, Vector2 incposition, Rectangle containerRect, System.Func<int> UIItemAction)
        {
            scale = s;
            font = incfont;
            popUpMenu = new ConstructionMenu(scale, font, stringList, incposition);

            size = (font.MeasureString(value.ToString())) * scale;
            valuePosition = new Vector2(0, (32 - size.Y) / 2);

            position = incposition;
            topLeft = tl;

            color = Color.White;
        }
        /*/
        public UIItem(float s, List<string> stringList, SpriteFont incfont, Vector2 tl, Vector2 incposition, Texture2D txt)
        {
            scale = s;
            float totHeight = 0f;
            float maxWidth = 0f;
            
            foreach (string st in stringList)
            {
                Vector2 tempVect = (incfont.MeasureString(st) * scale);
                //totHeight += (tempVect.Y);
                totHeight += 20;
                if (tempVect.X > maxWidth)
                {
                    maxWidth = tempVect.X;
                }
            }
            maxWidth += 4f;
            Vector2 center = new Vector2(maxWidth / 2, incposition.Y - (totHeight / 2));
            popUpMenu = new ConstructionMenu(scale, incfont, stringList, center);
            textureScale = new Vector2(maxWidth, totHeight);
            
            texture = txt;

            position = new Vector2(0f, incposition.Y - totHeight);
            topLeft = tl;

            color = Color.White;
            active = false;
            size = new Vector2((float)texture.Width, (float)texture.Height);
        }

        public void Update(MouseState state, MouseState prevState, int val)
        {
            value = val;
            //menuItem update method checks if the mouse is over it
            if (state.Position.X > position.X && state.Position.X < position.X + size.X && state.Position.Y > position.Y && state.Position.Y < position.Y + size.Y)
            {
                //if clicked run the proper function
                if (state.LeftButton == ButtonState.Released)// && prevState.LeftButton == ButtonState.Pressed)
                {
                    //run through the possible actions and find the one that isn't null. run that one
                    if (Action1 != null)
                    {
                        int i = Action1();
                    }
                    else if (Action2 != null)
                    {
                        active = !active;
                        int i = Action2(active);
                    }
                }
            }
        }

        public void Update(MouseState state, MouseState prevState)
        {
            //menuItem update method checks if the mouse is over it
            if (IsItemHovered(state))
            {
                //if clicked run the proper function
                if (state.LeftButton == ButtonState.Released && prevState.LeftButton == ButtonState.Pressed)
                {
                    //run through the possible actions and find the one that isn't null. run that one
                    if (Action1 != null)
                    {
                        int i = Action1();
                    }
                    else if (Action2 != null)
                    {
                        active = !active;
                        int i = Action2(active);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(popUpMenu != null)
            {
                if (active)
                {
                    spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, textureScale, SpriteEffects.None, 0f);
                    foreach (MenuItem item in popUpMenu.menuItemList)
                    {
                        spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition(), item.GetColor() * item.alpha, 0.0f, item.GetOrigin(), scale, SpriteEffects.None, 0.0f);
                    }
                }
            }
            else
            {
                spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, textureScale, SpriteEffects.None, 0f);
                if (font != null)
                {
                    spriteBatch.DrawString(font, value.ToString(), valuePosition, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
                }
            }
        }

        bool IsItemHovered(MouseState state)
        {
            //determine mouse position relative to this item
            return (state.Position.X > position.X && state.Position.X < position.X + size.X && state.Position.Y > position.Y && state.Position.Y < position.Y + size.Y);            
        }
    }
}
