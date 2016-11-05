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
        string readOut;
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
        /*/
        MouseState MouseState;
        MouseState LastMouseState;
        KeyboardState KeyState;
        KeyboardState LastKeyState;
        Vector2 MousePosition;
        /*/

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

        public UIItem(Vector2 tl, Vector2 incposition, Vector2 textureScale, Texture2D txt, bool active)
        {
            position = incposition;
            topLeft = tl;
            texture = txt;
            this.textureScale = textureScale;
            color = Color.White;
            this.active = active;
            size = new Vector2((float)texture.Width, (float)texture.Height);
        }

        public UIItem(float scale, int v, int vl, Vector2 tl, Vector2 incposition, Texture2D txt, System.Func<int> UIItemAction)
        {
            this.scale = scale;
            value = v;
            font = MenuController.Instance.font;
            readOut = v.ToString() + " / " + vl.ToString();
            size = font.MeasureString(readOut) * scale;
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
            //float x = ConstructionMenu.Instance.center.X - (ConstructionMenu.Instance.maxWidth / 2);
            //float y = ConstructionMenu.Instance.center.Y - (ConstructionMenu.Instance.totHeight / 2);
            //position = new Vector2(0, y);
            color = Color.White;

            Action2 = UIItemAction;
            active = false;
            size = textureScale;

            popUpMenu = MenuController.Instance.currentMenu;
        }

        public void Update(int v, int vl)
        {
            readOut = v.ToString() + " / " + vl.ToString();
            /*/
            this.MousePosition = InputManager.Instance.MousePosition;
            this.MouseState = InputManager.Instance.MouseState;
            this.LastMouseState = InputManager.Instance.LastMouseState;
            this.KeyState = InputManager.Instance.KeyState;
            this.LastKeyState = InputManager.Instance.LastKeyState;
            /*/
            //menuItem update method checks if the mouse is over it
            if (IsItemHovered()) 
            {
                //if clicked run the proper function
                if (InputManager.Instance.MouseState.LeftButton == ButtonState.Released && InputManager.Instance.LastMouseState.LeftButton == ButtonState.Pressed)
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

        public void Update()
        {
            //string s = "";
            if (popUpMenu != null && active)
            {
                //s = MenuController.Instance.Update();
                //UI.Instance.buildingSelected = s;
            }
            //menuItem update method checks if the mouse is over it
			if (IsItemHovered() && active)
            {
                //if clicked run the proper function
                if (InputManager.Instance.MouseState.LeftButton == ButtonState.Released && InputManager.Instance.LastMouseState.LeftButton == ButtonState.Pressed)
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
                    spriteBatch.DrawString(font, readOut, valuePosition, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
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

        bool IsItemHovered()
        {
            //might need to transform screen to map or map to screen or maybe scale
            //determine mouse position relative to this item
            Vector2 mp = InputManager.Instance.MousePosition;
            return (mp.X > position.X && mp.X < position.X + size.X && mp.Y > position.Y && mp.Y < position.Y + size.Y);            
        }
    }
}
