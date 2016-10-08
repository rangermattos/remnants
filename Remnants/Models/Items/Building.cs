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
        public float elapsedBuildTime;
        public int metalCost;
        public int powerCost;
        public int deltaPower;
        public float alpha;
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
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            /*
            var deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (alpha < 1f)
            {
                elapsedBuildTime += deltaT;
                alpha = (elapsedBuildTime / buildTime);
                if (alpha > 1f)
                    alpha = 1f;
            }
            else
                constructed = true;
            */
            spriteBatch.Draw(texture, Position);
        }

        public virtual void Place()
        {

        }
    }
}