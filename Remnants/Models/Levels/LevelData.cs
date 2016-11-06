using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remnants
{
    public class LevelData
    {
        [Serializable]
        public struct tileData
        {
            public string type;
            public int xPosition;
            public int yPosition;
            public bool canBuild;
            public bool canWalk;

            public tileData(int i, int j, Tile t)
            {
                type = t.GetType().Name;
                xPosition = i;
                yPosition = j;
                this.canBuild = t.canBuild;
                this.canWalk = t.canWalk;
            }
        }

        public struct buildingData
        {
            public string type;
            public Vector2 Position;
            public int status;
            public float buildTime;
            public float elapsedProductionTime;
            public float alpha;

            public buildingData(Building b)
            {
                type = b.GetType().Name;
                this.Position = b.Position;
                this.status = b.status;
                this.buildTime = b.buildTime;
                this.elapsedProductionTime = b.elapsedProductionTime;
                this.alpha = b.alpha;
            }
        }

        public int energy;
        public int metal;
        public int pop;
        public int food;
        public int water;
        public int antimatter;
        public int wood;
        public int nuclear;

        public List<buildingData> buildingList = new List<buildingData>();
        public List<tileData> tileList = new List<tileData>();
        public List<int> resourceList = new List<int>();
        public List<int> resourceLimits = new List<int>();
		public string[] resourceNames = new string[8] { "food", "water", "energy", "nuclear fuel", "antimatter", "wood", "metal", "population"};

		public int POP_PER_BUILDING = 2;

        public Vector2 mapSize;
        
        [NonSerialized]
        private static LevelData instance;

        private LevelData()
        {
            food = 10000;
            water = 10000;
			energy = 10000;
			nuclear = 10000;
            antimatter = 10000;
            wood = 10000;
            metal = 10000;
            pop = 10000;

            resourceList.Add(food);
            resourceList.Add(water);
			resourceList.Add(energy);
			resourceList.Add(nuclear);
            resourceList.Add(antimatter);
            resourceList.Add(wood);
            resourceList.Add(metal);
            resourceList.Add(pop);

            resourceLimits.Add(food);
            resourceLimits.Add(water);
			resourceLimits.Add(energy);
			resourceLimits.Add(nuclear);
            resourceLimits.Add(antimatter);
            resourceLimits.Add(wood);
            resourceLimits.Add(metal);
            resourceLimits.Add(pop);
        }

        public static LevelData Instance
        {
            get
            {
                if (instance == null)
                    instance = new LevelData();

                return instance;
            }
        }

        public void SetLevelData(LevelData d)
        {
            instance = d;
        }

        public void Update()
        {
            for (int i = 0; i < 8; i++)
            {
                if(resourceList[i] > resourceLimits[i])
                {
                    resourceList[i] = resourceLimits[i];
                }
				else if (resourceList[i] < 0)
				{
					resourceList[i] = 0;
				}
            }
            /*/
            resourceList[0] = food;
            resourceList[1] = water;
            resourceList[2] = energy;
            resourceList[3] = antimatter;
            resourceList[4] = nuclear;
            resourceList[5] = wood;
            resourceList[6] = metal;
            resourceList[7] = pop;
            /*/
        }

        public void SetLimits(int value)
        {
            for(int i = 0; i < 8; i++)
            {
                resourceLimits[i] = value;
            }
        }

		// returns whether sufficient resources to apply resourceUse
		public bool checkResources(int[] resourceUse)
		{
			for (int i = 0; i < 8; i++)
			{
				if (resourceList[i] < resourceUse[i])
				{
					Console.Write("Insufficient " + resourceNames[i] + "\n");
					return false;
				}
			}
			return true;
		}

		public bool canPopGrow()
		{
			return resourceList[(int)resources.POP] < resourceLimits[(int)resources.POP];
		}
    }
}
