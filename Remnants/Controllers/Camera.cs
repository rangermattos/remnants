using System;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Remnants
{

    public class Camera
    {
        private static Camera instance;

        public Camera2D cam;
        public Matrix viewportScale;
        public ViewportAdapter vp;

        private Camera() { }
        private Camera(ViewportAdapter viewportAdapter)
        {
            vp = viewportAdapter;
            cam = new Camera2D(viewportAdapter);
            viewportScale = viewportAdapter.GetScaleMatrix();
            cam.Zoom = 0.75f;
        }
        public static Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("Object not created");
                }
                return instance;
            }
        }

        public static void Create(ViewportAdapter viewportAdapter)
        {
            if (instance != null)
            {
                throw new Exception("Object already created");
            }
            instance = new Camera(viewportAdapter);
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = InputManager.Instance.KeyState;

            if (LevelController.Instance.levelOpen)
            {
                // movement
                if (keyboardState.IsKeyDown(Keys.W))
                    cam.Position -= new Vector2(0, 250) * deltaTime;

                if (keyboardState.IsKeyDown(Keys.S))
                    cam.Position += new Vector2(0, 250) * deltaTime;

                if (keyboardState.IsKeyDown(Keys.A))
                    cam.Position -= new Vector2(250, 0) * deltaTime;

                if (keyboardState.IsKeyDown(Keys.D))
                    cam.Position += new Vector2(250, 0) * deltaTime;

                if (InputManager.Instance.PressRelease(Keys.OemPlus))
                    cam.ZoomIn(0.1f);

                if (InputManager.Instance.PressRelease(Keys.OemMinus))
                    cam.ZoomOut(0.1f);
            }
            //cam.Position = Vector2.Transform(cam.Position, cam.GetViewMatrix());
            /*/
            var v = Vector2.Transform(Vector2.Zero, cam.GetViewMatrix());
            if (cam.Position.X  < 0)
                cam.Position = new Vector2(0, cam.Position.Y);

            if (cam.Position.X > LevelData.Instance.mapSize.X * 64)
                cam.Position = new Vector2(LevelData.Instance.mapSize.X * 64, cam.Position.Y);

            if (cam.Position.Y < 0 - (32 * viewportScale.Scale.Y))
                cam.Position = new Vector2(cam.Position.X, 0 - (32 * viewportScale.Scale.Y));

            if (cam.Position.Y > LevelData.Instance.mapSize.X * 64)
                cam.Position = new Vector2(cam.Position.X, LevelData.Instance.mapSize.Y * 64);
            /*/
        }
    }
}
