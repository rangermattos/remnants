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

        public bool PressRelease(Keys k)
        {
            return (KeyState.IsKeyUp(k) && LastKeyState.IsKeyDown(k));
        }

        public bool SpacePressRelease()
        {
            return (KeyState.IsKeyUp(Keys.Space) && LastKeyState.IsKeyDown(Keys.Space));
        }

        public bool NumPadPressRelease(int n)
		{
			switch (n)
			{
			case 1:
				return (KeyState.IsKeyUp(Keys.NumPad1) && LastKeyState.IsKeyDown(Keys.NumPad1));
			case 2:
				return (KeyState.IsKeyUp(Keys.NumPad2) && LastKeyState.IsKeyDown(Keys.NumPad2));
			case 3:
				return (KeyState.IsKeyUp(Keys.NumPad3) && LastKeyState.IsKeyDown(Keys.NumPad3));
			case 4:
				return (KeyState.IsKeyUp(Keys.NumPad4) && LastKeyState.IsKeyDown(Keys.NumPad4));
			case 5:
				return (KeyState.IsKeyUp(Keys.NumPad5) && LastKeyState.IsKeyDown(Keys.NumPad5));
			case 6:
				return (KeyState.IsKeyUp(Keys.NumPad6) && LastKeyState.IsKeyDown(Keys.NumPad6));
			case 7:
				return (KeyState.IsKeyUp(Keys.NumPad7) && LastKeyState.IsKeyDown(Keys.NumPad7));
			case 8:
				return (KeyState.IsKeyUp(Keys.NumPad8) && LastKeyState.IsKeyDown(Keys.NumPad8));
			case 9:
				return (KeyState.IsKeyUp(Keys.NumPad9) && LastKeyState.IsKeyDown(Keys.NumPad9));
			case 0:
				return (KeyState.IsKeyUp(Keys.NumPad0) && LastKeyState.IsKeyDown(Keys.NumPad0));
			default:
				Console.Write("Unknown numpad key: " + n + "\n");
				return false;
			}

		}
    }
}
