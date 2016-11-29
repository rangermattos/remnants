using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Remnants
{
    class MenuItem
    {
        public string text;
        Vector2 size = new Vector2();
        public Vector2 origin = new Vector2();
        SpriteFont font;
        public Color color;
        public Vector2 position = new Vector2();
        public float alpha;
        public float scale { get; set; }
        public int sign = -1;
        System.Func<Game1, int> Action1;
        System.Func<Game1, MenuController, int> Action2;
        System.Func<string> Action3;
        System.Func<int> Action4;
        MouseState prevState;
        bool offset = true;
        Building buildingRepresented;

        /*
         * MenuItems take functions as arguments
         * this is so I don't have to write every possible menuItem action methos in the menuitem class
         * it gives the class a bit more flexebility
         * this does however require a System.Func<> for each function that takes different arguments
         * each of these also needs a different constructor
         * there may be a better way to do this, but I'm moving on
        */

        public MenuItem(string inctext, SpriteFont incfont, Vector2 incposition)
        {
            scale = 1f;
            text = inctext;
            font = incfont;
            size = font.MeasureString(text) * scale;
            origin = size * 0.5f;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
        }

        public MenuItem(string inctext, SpriteFont incfont, Vector2 incposition, System.Func<Game1, int> menuItemAction)
        {
            scale = 1f;
            text = inctext;
            font = incfont;
            size = font.MeasureString(text) * scale;
            origin = size * 0.5f;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
            Action1 = menuItemAction;
        }

        public MenuItem(string inctext, SpriteFont incfont, Vector2 incposition, System.Func<int> menuItemAction)
        {
            scale = 1f;
            text = inctext;
            font = incfont;
            size = font.MeasureString(text) * scale;
            origin = size * 0.5f;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
            Action4 = menuItemAction;
        }
        /*/
        public MenuItem(float scale, string inctext, SpriteFont incfont, Vector2 incposition, System.Func<int> menuItemAction)
        {
            text = inctext;
            font = incfont;
            size = font.MeasureString(text) * scale;
            origin = size * scale;
            font = incfont;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
            Action3 = menuItemAction;
        }
        /*/
        public MenuItem(Building b, float sc, string inctext, SpriteFont incfont, Vector2 incposition, System.Func<string> menuItemAction)
        {
            buildingRepresented = b;
            scale = sc;
            text = inctext;
            font = incfont;
			size = font.MeasureString(text) * scale;
            origin = size * 0.5f;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
            Action3 = menuItemAction;
            offset = false;
        }

        public MenuItem(string inctext, SpriteFont incfont, Vector2 incposition, System.Func<Game1, MenuController, int> menuItemAction)
        {
            scale = 1f;
            text = inctext;
            font = incfont;
            size = font.MeasureString(text) * scale;
            origin = size * 0.5f;
            font = incfont;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
            Action2 = menuItemAction;

            prevState = Mouse.GetState();
        }

        public string Update(Game1 game, MenuController menuController)
        {
            //menuItem update method checks if the mouse is over it
            //mousePosition = mousePosition / game.Scale;
            if (buildingRepresented != null)
            {
                color = Color.White;
                for (int i = 0; i < 8; i++)
                {
                    if (buildingRepresented.resourceCost[i] > LevelData.Instance.resourceList[i])
                    {
                        color = Color.Red;
                        break;
                    }
                }
            }
            if (IsItemActive())
            {
                //pulse effect if hovered over
                Pulse();
                //if clicked run the proper function

                if (InputManager.Instance.LeftPressRelease())
                {
                    //run through the possible actions and find the one that isn't null. run that one
                    if (Action1 != null)
                    {
                        int i = Action1(game);
                    }
                    else if (Action2 != null)
                    {
                        int i = Action2(game, menuController);
                    }
                    else if (Action3 != null)
                    {
                        string s = Action3();
                        Console.WriteLine("action 3 returning: " + s);
                        return s;
                    }
                    else if (Action4 != null)
                    {
                        Action4();
                    }
                }
            }
            else
            {
                //if the mouse isn't over this menuItem, reset the pulse variables
                alpha = 1.0f;
                sign = -1;
            }
            return "";
        }
        /*
        public string Update(MouseState state)
        {
            //menuItem update method checks if the mouse is over it
            Vector2 mousePosition = new Vector2(state.X, state.Y);
            if (IsItemActive(mousePosition))
            {
                //pulse effect if hovered over
                Pulse();
                //if clicked run the proper function
                if (state.LeftButton == ButtonState.Released && prevState.LeftButton == ButtonState.Pressed)
                {
                    //run through the possible actions and find the one that isn't null. run that one
                    if (Action3 != null)
                    {
                        return Action3();
                    }
                }
            }
            else
            {
                //if the mouse isn't over this menuItem, reset the pulse variables
                alpha = 1.0f;
                sign = -1;
            }
            prevState = state;
            return "";
        }
        */
        
        public void Pulse()
        {
            alpha += sign * 0.01f;
            if (alpha <= 0.0f || alpha >= 1.0f)
            {
                sign = sign * -1;
            }
        }

        bool IsItemActive()
        {
            //determine mouse position relative to this item
            //Vector2 mp = Vector2.Transform(InputManager.Instance.MousePosition, Matrix.Invert(Camera.Instance.viewportScale));
            Vector2 mp = InputManager.Instance.MousePosition;
            
            if (offset)
            {
                return (mp.X > (position.X - (size.X / 2)) && mp.X < (position.X + (size.X / 2)) && mp.Y > (position.Y - (size.Y / 2)) && mp.Y < (position.Y + (size.Y / 2)));
            }
            return (mp.X > position.X && mp.X < position.X + size.X && mp.Y > position.Y && mp.Y < position.Y + size.Y);
            //return (mp.X > position.X && mp.X < position.X + size.X && mp.Y > position.Y && mp.Y < position.Y + size.Y);
        }

        public string GetText()
        {
            return text;
        }

        public Vector2 GetSize()
        {
            return size;
        }

        public Vector2 GetOrigin()
        {
            return origin;
        }

        public SpriteFont GetFont()
        {
            return font;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Color GetColor()
        {
            return color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GetFont(), GetText(), GetPosition(), GetColor() * alpha, 0.0f, GetOrigin(), scale, SpriteEffects.None, 0.0f);
            /*
            if (scale == null || scale == 1f)
            {
                spriteBatch.DrawString(GetFont(), GetText(), GetPosition(), GetColor() * alpha, 0.0f, GetOrigin(), 1.0f, SpriteEffects.None, 0.0f);
            }
            else
            {
                spriteBatch.DrawString(GetFont(), GetText(), GetPosition() - GetOrigin(), GetColor() * alpha, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
            }
            */
        }
    }
}
