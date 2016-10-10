using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
    class Line
    {
        public Vector2 A;
        public Vector2 B;
        public float Thickness;

        public Line() { }
        public Line(Vector2 a, Vector2 b, float thickness = 1)
        {
            A = a;
            B = b;
            Thickness = thickness;
        }

        public void Draw(SpriteBatch spriteBatch, Color color, Texture2D halfCircle, Texture2D lightningSegment)
        {
            Vector2 tangent = B - A;
            float rotation = (float)Math.Atan2(tangent.Y, tangent.X);

            const float ImageThickness = 8;
            float thicknessScale = Thickness / ImageThickness;

            Vector2 capOrigin = new Vector2(halfCircle.Width, halfCircle.Height / 2f);
            Vector2 middleOrigin = new Vector2(0, lightningSegment.Height / 2f);
            Vector2 middleScale = new Vector2(tangent.Length() / 10, thicknessScale);

            spriteBatch.Draw(lightningSegment, A, null, color, rotation, middleOrigin, middleScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(halfCircle, A, null, color, rotation, capOrigin, thicknessScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(halfCircle, B, null, color, rotation + MathHelper.Pi, capOrigin, thicknessScale, SpriteEffects.None, 0f);
        }
    }
}
