using System;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework;

namespace Remnants
{

    public class Camera
    {
        private static Camera instance;

        public Camera2D cam;
        public Matrix viewportScale;

        private Camera() { }
        private Camera(ViewportAdapter viewportAdapter)
        {
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
    }
}
