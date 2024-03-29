﻿using Microsoft.Xna.Framework;
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
        public int workersNeeded = 1;
        public int[] resourceCost;
        public int[] resourceStorage;
        public int[] resourceGain;
		public int[] resourceUsage;
        public int[] resourceChanges;
        public float buildTime;
        public float elapsedProductionTime = 0f;
        public int status = -1;
		public enum buildingStates { CONSTRUCTING, OPERATIONAL, IDLE, DISABLED };
        public ProgressBar progressBar;
		protected bool canDisable = true;

		public Building(ContentManager Content) : base(Content)
        {
            team = 1;
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

		public override void Update(GameTime gameTime, Level level)
        {
            
            var deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			switch(status)
			{
			case ((int)buildingStates.CONSTRUCTING): // still constructing
				alpha += deltaT / buildTime;
				if (alpha > 1f) // construction complete
				{
					alpha = 1f;
					completeConstruction(level); // should trigger just once
				}
				progressBar.progress += deltaT / buildTime;
				if (progressBar.progress > 1f)
					progressBar.progress = 1f;
				progressBar.barScale.X = progressBar.progress * 32;
				break;
			case ((int)buildingStates.OPERATIONAL):
                    mask = Color.White;
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
						//Console.Write("Sufficient resources, resuming operation\n");
						enable();
					}
					elapsedProductionTime = 0;
				}
				break;
			case ((int)buildingStates.DISABLED):
                    mask = Color.Red;
				break;
			}
			base.Update(gameTime, level);
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
            if (!CanPlace(map))
            {
                return false;
            }
            //if they can, set all of these tiles canBuild and canWalk variable to false so we can't overlap buildings
            SwapTileBools(map);
            for (int i = 0; i < 8; i++)
            {
                //subtract resource cost from available resources
                LevelData.Instance.resourceList[i] -= resourceCost[i];
            }
            //return true so level will build the building
            //SwapTileWalkBool(map);
            return true;
        }

        public bool CanPlace(Map map)
        {
            for (int i = 0; i < tilesWide; i++)
            {
                for (int j = 0; j < tilesHigh; j++)
                {
                    if (map.GetTile(position + new Vector2(i * 64, j * 64)) == null)
                    {
                        return false;
                    }
                    if (!map.GetTile(position + new Vector2(i * 64, j * 64)).canBuild)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        void SwapTileBools(Map map)
        {
            for (int i = 0; i < tilesWide; i++)
            {
                for (int j = 0; j < tilesHigh; j++)
                {
                    map.GetTile(position + new Vector2(i * 64, j * 64)).canBuild = !map.GetTile(position + new Vector2(i * 64, j * 64)).canBuild;
                    //map.GetTile(position + new Vector2(i * 64, j * 64)).canWalk = !map.GetTile(position + new Vector2(i * 64, j * 64)).canWalk;
                    //map.tiles[i][j].canBuild = !map.tiles[i][j].canBuild;
                    //map.tiles[i][j].canWalk = !map.tiles[i][j].canWalk;
                }
            }
        }

		void completeConstruction(Level level)
		{
			UI.Instance.EnqueueMessage("Construction completed");

            if(canDisable)
                status = (int)buildingStates.DISABLED;
            else
            {
                status = (int)buildingStates.OPERATIONAL;
            }

            for (int i = 0; i < 8; i++)
			{
				//add buildings storage capacity to resource limits
				LevelData.Instance.resourceLimits[i] += resourceStorage[i];
			}
            if ((LevelData.Instance.resourceList[(int)resources.POP] - LevelData.Instance.EmployedPopulation >= workersNeeded)){
                enable();
            }
		}

		public void enable()
		{
			if (isConstructing() || !isDisabled())
			{
				return;
			}
            if (!(LevelData.Instance.resourceList[(int)resources.POP] - LevelData.Instance.EmployedPopulation >= workersNeeded))
            {
                UI.Instance.EnqueueMessage("You don't have any unemployed citizens to work at this building");
                return;
            }


                // enable storage
            for (int i = 0; i < 7; i++) // exclude pop
			{
				LevelData.Instance.resourceLimits[i] += resourceStorage[i];
			}

			mask = Color.White;
			status = (int)buildingStates.OPERATIONAL;
            LevelData.Instance.EmployedPopulation += workersNeeded;
		}

		public void disable()
		{
			if (isConstructing() || !canDisable)
			{
				return;
			}

			// disable storage
			for (int i = 0; i < 7; i++) // exclude pop
			{
				LevelData.Instance.resourceLimits[i] -= resourceStorage[i];
			}

			mask = Color.Red;
            if (status == (int)buildingStates.OPERATIONAL)
                LevelData.Instance.EmployedPopulation -= workersNeeded;
			status = (int)buildingStates.DISABLED;
		}

		public void idle()
		{
			if (isConstructing() || !canDisable)
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