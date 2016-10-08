using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    public class Camera2D
    {
        Vector2 mapSize;
        private readonly Viewport _viewport;

        public Camera2D(Viewport viewport, Vector2 ms)
        {
            _viewport = viewport;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
            mapSize = ms;
        }
        
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public Vector2 Origin { get; set; }

        public Matrix GetViewMatrix()
        {
            //so the camera can't go off map
            if ((Position.X + Origin.X) - Origin.X / Zoom < 0)
                Position = new Vector2(0 - Origin.X + (Origin.X / Zoom), Position.Y);
            if(Position.X > mapSize.X - (Origin.X * 2f) / Zoom)
                Position = new Vector2(mapSize.X - (Origin.X * 2f) / Zoom, Position.Y);
            if (Position.Y + Origin.Y - Origin.Y / Zoom < 0)
                Position = new Vector2(Position.X, 0 - Origin.Y + (Origin.Y / Zoom));
            if (Position.Y > mapSize.Y - (Origin.Y * 2f) / Zoom)
                Position = new Vector2(Position.X, mapSize.Y - (Origin.Y * 2f) / Zoom);


            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}
