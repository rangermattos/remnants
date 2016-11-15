using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Remnants.Models;

namespace Remnants
{
    public class Building : Entity
    {
        public int tilesWide;
        public int tilesHigh;
        public int populationHousing = 0;
        public int[] resourceCost;
        public int[] resourceStorage;
        protected int[] resourceGain;
		protected int[] resourceUsage;
        public int[] resourceChanges;
        public float buildTime;
        public float elapsedProductionTime = 0f;
        public int status = -1;
		public enum buildingStates { CONSTRUCTING, OPERATIONAL, IDLE, DISABLED };
        public ProgressBar progressBar;

		public Building(ContentManager Content) : base(Content)
        {
            resourceGain = new int[8];
			resourceUsage = new int[8];
            resourceCost = new int[8];
            resourceChanges = new int[8];
            resourceStorage = new int[8];
            for (int i = 0; i < 8; i++)
            {
                resourceGain[i] = 0;
				resourceUsage[i] = 0;
                resourceChanges[i] = 0;
                resourceCost[i] = 0;
                resourceStorage[i] = 0;
            }
			if (status == -1)
				status = (int)buildingStates.CONSTRUCTING;

			width = tilesWide * 64;
			height = tilesHigh * 64;
			Init();
        }
        public override void Init()
        {
            base.Init();
            this.hp = 1000;
            this.hpMax = 1000;
            this.attackStrength = 10;
            this.defenseStrength = 10;
        }
        public virtual void LoadContent(ContentManager Content)
        {
			var p = new Vector2(position.X + tilesWide * 32, position.Y);
			progressBar = new ProgressBar(Content, p, p);
            progressBar.position = new Vector2(progressBar.position.X - progressBar.container.Width / 2, progressBar.position.Y);
			healthBar.position = new Vector2(p.X - healthBar.container.Width/2, p.Y);
        }

        public virtual void UnloadContent()
        {

        }

		public override void Update(GameTime gameTime)
        {
            
            var deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			switch(status)
			{
			case ((int)buildingStates.CONSTRUCTING): // still constructing
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
				break;
			case ((int)buildingStates.OPERATIONAL):
				// calculate production/cost
				elapsedProductionTime += deltaT;
				if (elapsedProductionTime >= 1f)
				{
					// check if enough resources to operate
					if (LevelData.Instance.checkResources(resourceUsage))
					{
						for (int i = 0; i < 8; i++)
						{
							LevelData.Instance.resourceList[i] += resourceGain[i];
							LevelData.Instance.resourceList[i] -= resourceUsage[i];
						}
					}
					else
					{
						idle();
					}
					elapsedProductionTime = 0;
				}

				if (animated)
				{
					currentAnimation.Update(gameTime);
				}
				break;
			case ((int)buildingStates.IDLE):
				elapsedProductionTime += deltaT;
				if (elapsedProductionTime >= 1f)
				{
					if (LevelData.Instance.checkResources(resourceUsage))
					{
						Console.Write("Sufficient resources, resuming operation\n");
						enable();
					}
					elapsedProductionTime = 0;
				}
				break;
			case ((int)buildingStates.DISABLED):

				break;
			}
			base.Update(gameTime);
        }

        public virtual void Update(GameTime gameTime, Vector2 point)
        {

        }

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			if (progressBar.progress != 1.0f)
			{
				progressBar.Draw(spriteBatch);
			}
        }

        public virtual bool Place(Map map)
        {
			if (!LevelData.Instance.checkResources(resourceCost))
			{
				UI.Instance.EnqueueMessage("Cannot construct building");
                return false;
			}
            //check that all tiles the building will be on can be built on
            for(int i = 0; i < tilesWide; i++)
            {
                for(int j = 0; j < tilesHigh; j++)
                {
                    if(map.GetTile(position + new Vector2(i * 64, j * 64)) == null)
                    {
                        return false;
                    }
                    if (!map.GetTile(position + new Vector2(i * 64, j * 64)).canBuild)
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
                    map.GetTile(position + new Vector2(i * 64, j * 64)).canBuild = false;
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

		void completeConstruction()
		{
			UI.Instance.EnqueueMessage("Construtction completed");
			for (int i = 0; i < 8; i++)
			{
				//add buildings storage capacity to resource limits
				LevelData.Instance.resourceLimits[i] += resourceStorage[i];
			}
			status = (int)buildingStates.OPERATIONAL;
		}

		public void enable()
		{
			if (isConstructing())
			{
				return;
			}
			mask = Color.White;
			status = (int)buildingStates.OPERATIONAL;
		}

		public void disable()
		{
			if (isConstructing())
			{
				return;
			}
			mask = Color.Red;
			status = (int)buildingStates.DISABLED;
		}

		public void idle()
		{
			if (isConstructing())
			{
				return;
			}
			mask = Color.Orange;
			status = (int)buildingStates.IDLE;
		}

		public bool isConstructing()
		{
			return status == (int)buildingStates.CONSTRUCTING;
		}

		public bool isDisabled()
		{
			return status == (int)buildingStates.DISABLED;
		}
    }
}