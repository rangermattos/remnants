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
        public int populationHousing = 0;
        public int[] resourceCost;
        public int[] resourceStorage;
        protected int[] deltas;
        public int[] resourceChanges;
        public float alpha;
        public float buildTime;
        public float elapsedProductionTime = 0f;
        public bool operational;
        public Vector2 Position { get; set; }
        ProgressBar progressBar;
        protected bool animated = false;
        protected Animation animation;

        public Building()
        {
            deltas = new int[8];
            resourceCost = new int[8];
            resourceChanges = new int[8];
            resourceStorage = new int[8];
            for (int i = 0; i < 8; i++)
            {
                deltas[i] = 0;
                resourceChanges[i] = 0;
                resourceCost[i] = 0;
                resourceStorage[i] = 0;
            }
        }

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
			if (alpha < 1f && !operational) // still constructing
			{
				alpha += deltaT / buildTime;
				if (alpha > 1f) // construction complete
				{
					alpha = 1f;
					completeConstruction(); // should trigger just once
				}
				progressBar.progress += deltaT / buildTime;
				if (progressBar.progress > 1f)
					progressBar.progress = 1f;
				progressBar.barScale.X = progressBar.progress * 32;
			} 
			else 
			{
				// calculate production/cost
				elapsedProductionTime += deltaT;
				if (elapsedProductionTime >= 1f)
				{
					/*
                    foodChange += deltaFood;
					waterChange += deltaWater;
					energyChange += deltaEnergy;
					woodChange += deltaWood;
					metalChange += deltaMetal;
                    */
					for (int i = 0; i < 8; i++)
					{
						LevelData.Instance.resourceList[i] += deltas[i];
					}
					elapsedProductionTime = 0;
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
            if (!CheckResources())
                return false;
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
            for (int i = 0; i < 8; i++)
            {
                //subtract resource cost from available resources
                LevelData.Instance.resourceList[i] -= resourceCost[i];
            }
            //return true so level will build the building
            return true;
        }

        bool CheckResources()
        {
            for(int i = 0; i < 8; i++)
            {
                if (LevelData.Instance.resourceList[i] < resourceCost[i])
                    return false;
            }
            return true;
        }

		void completeConstruction()
		{
			Console.Write("Construtction completed\n");
			for (int i = 0; i < 8; i++)
			{
				//add buildings storage capacity to resource limits
				LevelData.Instance.resourceLimits[i] += resourceStorage[i];
			}
		}
    }
}