using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

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
		ProgressBar progressBar;

        public virtual void LoadContent(ContentManager Content)
        {
			progressBar = new ProgressBar(Content, Position, Position);
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
			progressBar.position = Position;

            var deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (alpha < 1f) 
			{
				alpha += deltaT / buildTime;
				if (alpha > 1f)
					alpha = 1f;
				progressBar.progress += deltaT / buildTime;
				if (progressBar.progress > 1f)
					progressBar.progress = 1f;
				progressBar.barScale.X = progressBar.progress * 32;
			} 
			else 
			{
				operational = true;
			}

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

        public virtual void Update(GameTime gameTime, Vector2 point)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White * alpha);

			if (progressBar.progress != 1.0f)
			{
				spriteBatch.Draw(progressBar.container, progressBar.position);
				spriteBatch.Draw(progressBar.bar, progressBar.position, scale:progressBar.barScale);
			}
        }

        public virtual void Place()
        {

        }
    }
}