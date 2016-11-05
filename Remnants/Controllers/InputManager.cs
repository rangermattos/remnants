using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Remnants
{
    class InputManager
    {
        private static InputManager instance;
        public MouseState MouseState;
        public MouseState LastMouseState;
        public KeyboardState KeyState;
        public KeyboardState LastKeyState;
        public Vector2 MousePosition;

        private InputManager()
        {
            MouseState = Mouse.GetState();
            KeyState = Keyboard.GetState();
            LastMouseState = MouseState;
            LastKeyState = KeyState;
            MousePosition = new Vector2(MouseState.X, MouseState.Y);
        }

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        public void Update()
        {
            LastMouseState = MouseState;
            LastKeyState = KeyState;
            MouseState = Mouse.GetState();
            KeyState = Keyboard.GetState();
            MousePosition = new Vector2(MouseState.X, MouseState.Y);
        }

        public bool LeftPressRelease()
        {
            return (MouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed);
        }

        public bool RightPressRelease()
        {
            return (MouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed);
        }

        public bool EscPressRelease()
        {
            return (KeyState.IsKeyUp(Keys.Escape) && LastKeyState.IsKeyDown(Keys.Escape));
        }
    }
}
