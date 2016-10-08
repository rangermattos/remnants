using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    class Building
    {
        public Texture2D texture;
        public int tilesWide;
        public int tilesHigh;
        public float buildTime;
        public int metalCost;
        public int powerCost;
        public int deltaPower;
        public float alpha = 0f;
        public bool constructed;
        public Vector2 Position { get; set; }

        public virtual void LoadContent(ContentManager Content)
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            var deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (alpha < 1f)
            {
                alpha += deltaT / buildTime;
                if (alpha > 1f)
                    alpha = 1f;
            }
            else
                constructed = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White * alpha);
        }

        public virtual void Place()
        {

        }
    }
}