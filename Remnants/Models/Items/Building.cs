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
        public int metalCost;
        public int energyCost;
        protected int deltaEnergy;
        public int produced;
        public int energyChange;
        public float alpha = 0f;
        public float buildTime;
        public float elapsedTime = 0f;
        public bool operational;
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
                operational = true;

            if (operational)
            {
                elapsedTime += deltaT;
                if (elapsedTime >= 1f)
                {
                    energyChange += deltaEnergy;
                    elapsedTime -= 1f;
                }
            }
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