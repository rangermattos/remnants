using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using Remnants.Models;

namespace Remnants
{
    public class Building : Entity
    {
        public Texture2D texture;
        public int tilesWide;
        public int tilesHigh;
		public int woodCost = 0;
        public int metalCost = 0;
        public int energyCost = 0;
		public int foodStorage = 0;
		public int waterStorage = 0;
		public int woodStorage = 0;
		public int metalStorage = 0;
		public int energyStorage = 0;
		public int populationHousing = 0;
		protected int deltaFood = 0;
		protected int deltaWater = 0;
        protected int deltaEnergy = 0;
		protected int deltaWood = 0;
		protected int deltaMetal = 0;
        public int produced;
		public int foodChange;
		public int waterChange;
        public int energyChange;
		public int woodChange;
		public int metalChange;
        public float alpha = 0f;
        public float buildTime;
        public float elapsedProductionTime = 0f;
        public bool operational;
        public Vector2 Position { get; set; }
		ProgressBar progressBar;
		protected bool animated = false;
		protected Animation animation;

		public virtual void LoadContent(ContentManager Content)
        {
			var p = new Vector2(Position.X + tilesWide * 32, Position.Y);
			progressBar = new ProgressBar(Content, p, p);
            progressBar.position = new Vector2(progressBar.position.X - progressBar.container.Width / 2, progressBar.position.Y);
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
				// calculate production/cost
                elapsedProductionTime += deltaT;
                if (elapsedProductionTime >= 1f)
                {
                    foodChange += deltaFood;
					waterChange += deltaWater;
					energyChange += deltaEnergy;
					woodChange += deltaWood;
					metalChange += deltaMetal;
                    elapsedProductionTime -= 1f;
                }

				if (animated)
				{
					animation.Update(gameTime);
				}
            }
        }

        public virtual void Update(GameTime gameTime, Vector2 point)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
			if (animated)
			{
				animation.Draw(spriteBatch, Position, Color.White * alpha);
			}
			else
			{
            	spriteBatch.Draw(texture, Position, Color.White * alpha);
			}

			if (progressBar.progress != 1.0f)
			{
				spriteBatch.Draw(progressBar.container, progressBar.position);
				spriteBatch.Draw(progressBar.bar, progressBar.position, scale:progressBar.barScale);
			}
        }

        public virtual bool Place(Map map)
        {
            //check that all tiles the building will be on can be built on
            for(int i = 0; i < tilesWide; i++)
            {
                for(int j = 0; j < tilesHigh; j++)
                {
                    if(map.GetTile(Position + new Vector2(i * 64, j * 64)) == null)
                    {
                        return false;
                    }
                    if (!map.GetTile(Position + new Vector2(i * 64, j * 64)).canBuild)
                    {
                        return false;
                    }
                }
            }
            //if they can, set all of these tiles canBuild variable to false so we can't overlap buildings

            for (int i = 0; i < tilesWide; i++)
            {
                for (int j = 0; j < tilesHigh; j++)
                {
                    map.GetTile(Position + new Vector2(i * 64, j * 64)).canBuild = false;
                }
            }
            //return true so level will build the building
            return true;
        }
    }
}