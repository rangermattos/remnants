using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

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
        System.Func<int> Action3;
        //System.Func<string> Action3;
        public bool active { get; set; }
        Menu popUpMenu;

        public UIItem(Vector2 tl, Vector2 incposition, Texture2D txt, System.Func<int> UIItemAction)
        {
            position = incposition;
            topLeft = tl;
            texture = txt;
            textureScale = new Vector2(1f, 1f);
            color = Color.White;
            active = true;
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
            active = true;
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
            active = true;
            size = new Vector2((float)texture.Width, (float)texture.Height);
        }
        
        public UIItem(Vector2 tl, Texture2D txt, System.Func<bool, int> UIItemAction)
        {
            textureScale = new Vector2(ConstructionMenu.Instance.maxWidth, ConstructionMenu.Instance.totHeight);
            
            texture = txt;

            //position = new Vector2(0, (Camera.Instance.cam.Origin.Y * 2) - 32);
            //position = new Vector2(0f, position.Y - ConstructionMenu.totHeight);
            //topLeft = tl;
            float x = ConstructionMenu.Instance.center.X - (ConstructionMenu.Instance.maxWidth / 2);
            float y = ConstructionMenu.Instance.center.Y - (ConstructionMenu.Instance.totHeight / 2);
            position = new Vector2(0, y);
            color = Color.White;

            Action2 = UIItemAction;
            active = false;
            size = textureScale;

            popUpMenu = MenuController.Instance.currentMenu;
        }

        public void Update(MouseState state, MouseState prevState, int val)
        {
            value = val;
            //menuItem update method checks if the mouse is over it
            if (IsItemHovered(state)) 
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

        public void Update(MouseState state, MouseState prevState, UI ui)
        {
            string s = "";
            if (popUpMenu != null && active)
            {
                s = MenuController.Instance.Update();
                ui.buildingSelected = s;
            }
            //menuItem update method checks if the mouse is over it
			if (IsItemHovered(state) && active)
            {
                //if clicked run the proper function
                if (state.LeftButton == ButtonState.Released && prevState.LeftButton == ButtonState.Pressed)
				{
					if (popUpMenu != null)
						Console.Write("Construction menu clicked\n");
                    //run through the possible actions and find the one that isn't null. run that one
                    if (Action1 != null)
                    {
                        int i = Action1();
                    }
                    else if (Action2 != null)
                    {
                        //active = !active;
						Console.Write("Active: " + active + "\n");
                        int i = Action2(active);
                    }
                    else if (Action3 != null)
                    {
                        Action3();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, textureScale, SpriteEffects.None, 0f);
                if (font != null)
                {
                    spriteBatch.DrawString(font, value.ToString(), valuePosition, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
                }
            }
            /*
            if(popUpMenu != null)
            {
                if (active)
                {
                    spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, textureScale, SpriteEffects.None, 0f);
                    foreach (MenuItem item in popUpMenu.menuItemList)
                    {
						spriteBatch.DrawString(item.GetFont(), item.GetText(), item.GetPosition() - item.GetOrigin(), item.GetColor() * item.alpha, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
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
            */
        }

        bool IsItemHovered(MouseState state)
        {
            //determine mouse position relative to this item
            return (state.Position.X > position.X && state.Position.X < position.X + size.X && state.Position.Y > position.Y && state.Position.Y < position.Y + size.Y);            
        }
    }
}
