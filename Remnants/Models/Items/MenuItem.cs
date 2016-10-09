using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Remnants
{
    class MenuItem
    {
        string text;
        Vector2 size = new Vector2();
        Vector2 origin = new Vector2();
        SpriteFont font;
        Color color;
        public Vector2 position = new Vector2();
        public float alpha;
        public float scale { get; set; }
        public int sign = -1;
        System.Func<Game1, int> Action1;
        System.Func<Game1, MenuController, int> Action2;
        System.Func<int> Action3;

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
            text = inctext;
            font = incfont;
            size = font.MeasureString(text);
            origin = size * 0.5f;
            font = incfont;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
        }

        public MenuItem(string inctext, SpriteFont incfont, Vector2 incposition, System.Func<Game1, int> menuItemAction)
        {
            text = inctext;
            font = incfont;
            size = font.MeasureString(text);
            origin = size * 0.5f;
            font = incfont;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
            Action1 = menuItemAction;
        }
        /*/
        public MenuItem(float scale, string inctext, SpriteFont incfont, Vector2 incposition, System.Func<int> menuItemAction)
        {
            text = inctext;
            font = incfont;
            size = font.MeasureString(text);
            origin = size * scale;
            font = incfont;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
            Action3 = menuItemAction;
        }
        /*/
        public MenuItem(float sc, string inctext, SpriteFont incfont, Vector2 incposition)
        {
            scale = sc;
            text = inctext;
            font = incfont;
            size = font.MeasureString(text) * scale;
            origin = size * 0.5f;
            font = incfont;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
        }

        public MenuItem(string inctext, SpriteFont incfont, Vector2 incposition, System.Func<Game1, MenuController, int> menuItemAction)
        {
            text = inctext;
            font = incfont;
            size = font.MeasureString(text);
            origin = size * 0.5f;
            font = incfont;
            alpha = 1.0f;
            position = incposition;
            color = Color.White;
            Action2 = menuItemAction;
        }

        public void Update(MouseState state, Game1 game, MenuController menuController)
        {
            //menuItem update method checks if the mouse is over it
            if (IsItemActive(state))
            {
                //pulse effect if hovered over
                Pulse();
                //if clicked run the proper function
                if (state.LeftButton == ButtonState.Pressed)
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
                }
            }
            else
            {
                //if the mouse isn't over this menuItem, reset the pulse variables
                alpha = 1.0f;
                sign = -1;
            }
        }

        public void Pulse()
        {
            alpha += sign * 0.01f;
            if (alpha <= 0.0f || alpha >= 1.0f)
            {
                sign = sign * -1;
            }
        }

        bool IsItemActive(MouseState state)
        {
            //determine mouse position relative to this item
            if (state.X > (position.X - (size.X / 2)) && state.X < (position.X + (size.X / 2)) && state.Y > (position.Y - (size.Y / 2)) && state.Y < (position.Y + (size.Y / 2)))
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }
}
