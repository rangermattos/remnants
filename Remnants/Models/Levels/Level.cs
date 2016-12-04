using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;
using Remnants.Models;

internal class tileCoords
{
    public tileCoords(Vector2 vec)
    {
        x = (int)(vec.X / 64);
        y = (int)(vec.Y / 64);
    }
    public tileCoords(tileCoords tc)
    {
        x = tc.x;
        y = tc.y;
    }
    public int x, y;
    public float gCost, fCost, hCost;
    public tileCoords last;
    public override int GetHashCode()
    {
        return x * 391939 + y;
    }
    public int getHCostTo(tileCoords tc)
    {
        return (Math.Abs(x - tc.x) * 10) + (Math.Abs(y - tc.y) * 10);
    }
}
namespace Remnants
{
    public class Level
	{
		Random r = new Random();
        StorageDevice device;
        Map map;
        List<Building> buildings = new List<Building>();
        List<Unit> enemyUnits = new List<Unit>();
        SpriteFont font = MenuController.Instance.font;
		float lastPopulationGrowth = 0f;
		float meanGrowthTime = 10f;
		int[] populationGrowthCost = new int[8] { 20, 20, 0, 0, 0, 0, 0, 0 };
		float elapsedConsumptionTime = 0f;
        public bool paused = false;
        Texture2D buildingPlacementTexture;
        Color placementMask;
        float placementAlpha = 0.65f;
        Building tempBuilding;
        double UnitSpawnTime = 60;
        int UnitSpawnCount = 5;
        double timePassed = 0;
        int spawnIterations = 0;
        int difficulty;
        bool warned = false;
        bool landed = false;
        public int employedPopulation = 0;

        public Level()
        {
            LevelData.Instance.InitValues();
            LevelData.Instance.SetLimits(0);
			LevelData.Instance.resourceLimits[(int)resources.NUCLEAR] = 0;
			LevelData.Instance.resourceLimits[(int)resources.ANTIMATTER] = 0;
			LevelData.Instance.resourceLimits[(int)resources.POP] = 0;
            difficulty = LevelData.Instance.difficulty;
            map = new Map();
            Camera.Instance.cam.Position = LevelData.Instance.mapSize * 64 / 2;
            UnitSpawnCount *= LevelData.Instance.difficulty;
            UnitSpawnTime += 120 - 30 * LevelData.Instance.difficulty;
        }

        public Level(int diff)
        {
            LevelData.Instance.InitValues();
            LevelData.Instance.SetLimits(0);
            LevelData.Instance.resourceLimits[(int)resources.NUCLEAR] = 0;
            LevelData.Instance.resourceLimits[(int)resources.ANTIMATTER] = 0;
            LevelData.Instance.resourceLimits[(int)resources.POP] = 0;
            LevelData.Instance.difficulty = diff;
            difficulty = diff;
            map = new Map();
            Camera.Instance.cam.Position = LevelData.Instance.mapSize * 64 / 2;
            UnitSpawnCount *= LevelData.Instance.difficulty;
            UnitSpawnTime += 120 - 30 * LevelData.Instance.difficulty;
        }

        //level constructer with loadgame
        public Level(string filename)
        {
            MainMenu.Instance.isActive = false;
            Settings.Instance.isActive = false;
            LoadGame(filename);
            map = new Map(LevelData.Instance.mapSize);
            Camera.Instance.cam.Position = LevelData.Instance.mapSize * 64 / 2;
            UnitSpawnCount *= LevelData.Instance.difficulty;
            UnitSpawnTime += 120 - 30 * LevelData.Instance.difficulty;
        }

        public void LoadContent(ContentManager Content)
        {

            UI.Create(Content);
            if (LevelData.Instance.buildingList.Count > 0)
            {
                Console.WriteLine("loading " + LevelData.Instance.buildingList.Count + "buildings");
                LoadBuildings(Content);
            }
            else
            {
                UI.Instance.buildingSelected = "LanderBase";
            }
            map.LoadContent(Content);
            //UI.Instance.isActive = true;
        }
        
        void LoadBuildings(ContentManager Content)
        {
            object[] po = new object[2];
            po[0] = Content;
            foreach (LevelData.buildingData b in LevelData.Instance.buildingList)
            {
                po[1] = b.position;
                Building tempBuilding;
                try
                {
                    //magic?
                    tempBuilding = (Building)Activator.CreateInstance(Type.GetType("Remnants." + b.type), po);
                }
                catch (Exception)
                {
                    throw;
                }
                tempBuilding.status = b.status;
                tempBuilding.buildTime = b.buildTime;
                tempBuilding.elapsedProductionTime = b.elapsedProductionTime;
                tempBuilding.alpha = b.alpha;

                if (tempBuilding.status == (int)Building.buildingStates.CONSTRUCTING)
                {
                    var prog = new Vector2(tempBuilding.position.X + tempBuilding.tilesWide * 32, tempBuilding.position.Y);
                    tempBuilding.progressBar = new ProgressBar(Content, prog, prog);
                    tempBuilding.progressBar.position = new Vector2(tempBuilding.progressBar.position.X - tempBuilding.progressBar.container.Width / 2, tempBuilding.progressBar.position.Y);
                    tempBuilding.progressBar.progress = b.progress;
                }
                else
                {
                    var prog = new Vector2(tempBuilding.position.X + tempBuilding.tilesWide * 32, tempBuilding.position.Y);
                    tempBuilding.progressBar = new ProgressBar(Content, prog, prog);
                    tempBuilding.progressBar.position = new Vector2(tempBuilding.progressBar.position.X - tempBuilding.progressBar.container.Width / 2, tempBuilding.progressBar.position.Y);
                    tempBuilding.progressBar.progress = 1.0f;
                }
                //tempBuilding.Place(map);
                buildings.Add(tempBuilding);
            }
        }

        public void UnloadContent(ContentManager Content)
        {
            //game.Content.Unload();
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            var deltaT = gameTime.ElapsedGameTime.TotalSeconds;
            //if (InputManager.Instance.SpacePressRelease())
            LevelData.Instance.Update();
            UI.Instance.Update(gameTime, landed);

            Vector2 p = Camera.Instance.cam.ScreenToWorld(InputManager.Instance.MousePosition);
            if (!EscapeMenu.Instance.isActive)
            {
                if (landed && InputManager.Instance.PressRelease(Keys.Space))
                    paused = !paused;
                if(!UI.Instance.MouseOverUI())
                    CheckBuilding(p, Content);
            }

            if (!paused && landed)
            {
                timePassed += deltaT;

                if(timePassed >= UnitSpawnTime - 10)
                {
                    if (!warned)
                    {
                        UI.Instance.EnqueueMessage("WARNING: Attack incoming in 10 seconds!");
                        warned = true;
                    }
                }
                if (timePassed >= UnitSpawnTime)
                //if (InputManager.Instance.PressRelease(Keys.U))
                {
                    spawnUnits(Content);
                    warned = false;
                }

                EnableOrDisableBuilding(p);

                UpdateBuildings(gameTime, p);

                UpdatePopulation(gameTime);
                
                UpdateUnits(gameTime);
            }

            /*
            if (InputManager.Instance.PressRelease(Keys.U))
            {
                DefaultAlien testAlien = new DefaultAlien(Content);
                //testAlien.position = Vector2.Zero;
                testAlien.position = p;
                enemyUnits.Add(testAlien);
            }
            */

			//SetResources();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Camera.Instance.cam.GetViewMatrix());
            map.Draw(spriteBatch);
            if(tempBuilding != null)
            {
                //draw indicator of where building will be placed
                spriteBatch.Draw(tempBuilding.texture, tempBuilding.position, placementMask * placementAlpha);
            }
            foreach (Building b in buildings)
            {
                b.Draw(spriteBatch);
            }
            foreach(Unit u in enemyUnits)
            {
                u.Draw(spriteBatch);
            }
            spriteBatch.End();

            UI.Instance.Draw(spriteBatch);
        }

        public Vector2 getCameraVector()
        {
            return Camera.Instance.cam.ScreenToWorld(InputManager.Instance.MousePosition);
        }

        public void UpdateBuildings(GameTime gameTime, Vector2 p)
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                //unified update.... finally..... ...... ..... ......
                Building b = buildings[i];
                b.Update(gameTime, this);
                if(b.hp <= 0)
				{
					// set tiles to be buildable again
					for (int j = 0; j < b.tilesWide; j++)
					{
						for (int k = 0; k < b.tilesHigh; k++)
						{
							map.GetTile(b.position + new Vector2(j * 64, k * 64)).canBuild = true;
						}
					}
					int popDiff = LevelData.Instance.resourceLimits[(int)resources.POP] - (LevelData.Instance.resourceList[(int)resources.POP] - b.resourceStorage[(int)resources.POP]);
					if (popDiff > 0)
					{
						UI.Instance.EnqueueMessage(popDiff + " citizens were killed!");
					}
					for (int j = 0; j < 8; j++)
					{
						LevelData.Instance.resourceLimits[j] -= b.resourceStorage[j];
					}
                    buildings.RemoveAt(i);
                    i--;
                    UI.Instance.EnqueueMessage("Your " + b.name + " was destroyed");
                }
            }
        }

        public void UpdateUnits(GameTime gameTime)
        {
			for (int i = 0; i < enemyUnits.Count; i++)
            {
				enemyUnits[i].Update(gameTime, this);
				if (enemyUnits[i].hp <= 0) // if dead, remove
				{
					enemyUnits.RemoveAt(i);
					i--; // hack to go offset removal
					//UI.Instance.EnqueueMessage("Enemy killed");
				}
            }
        }

        public void CheckBuilding(Vector2 p, ContentManager Content)
        {
            string buildingString = UI.Instance.buildingSelected;

            if (tempBuilding != null)
            {
                //update position of temp building which is used in placement indicator
                int x = (int)Math.Floor(p.X / 64);
                int y = (int)Math.Floor(p.Y / 64);
                //scale back up, p will now be in line with the tiles
                p = new Vector2(x * 64f, y * 64f);
                tempBuilding.position = p;
                //if it can't be placed in that spot, give it a red hue
                if (tempBuilding.CanPlace(map))
                {
                    placementMask = Color.White;
                }
                else
                {
                    placementMask = Color.Red;
                }
            }

            if (buildingString != "")
            {
                //if we change our selected building to be build, make a new temp building
                if (tempBuilding == null)
                {
                    newTempBuilding(p, Content);
                }
                else if (("Remnants." + buildingString) != tempBuilding.GetType().ToString())
                {
                    newTempBuilding(p, Content);
                }
            }
            else if(buildingString == "")
            {
                buildingPlacementTexture = null;
            }
            if (buildingString != "" && InputManager.Instance.LeftPressRelease() )
            {
                tempBuilding = null;
                newTempBuilding(p, Content);

                if (tempBuilding.Place(map))
                {
                    tempBuilding.status = (int)Building.buildingStates.CONSTRUCTING;
                    var prog = new Vector2(tempBuilding.position.X + tempBuilding.tilesWide * 32, tempBuilding.position.Y);
                    tempBuilding.progressBar = new ProgressBar(Content, prog, prog);
                    tempBuilding.progressBar.position = new Vector2(tempBuilding.progressBar.position.X - tempBuilding.progressBar.container.Width / 2, tempBuilding.progressBar.position.Y);


                    if (tempBuilding.GetType().ToString() == "Remnants.LanderBase")
                    {
                        processLanding();
                    }

                    buildings.Add(tempBuilding);

                    LevelData.Instance.buildingList.Add(new LevelData.buildingData(tempBuilding));
                    tempBuilding = null;
                }
                else
                {
                    UI.Instance.EnqueueMessage("Cannot construct building on that location");
                }
                /*/
                if (buildings.Count < (LevelData.Instance.resourceList[(int)resources.POP] * LevelData.Instance.BUILDINGS_PER_POP)
					|| tempBuilding is SmallHouse || tempBuilding is MediumHouse || tempBuilding is LargeHouse || tempBuilding is LanderBase)
				{
				}
				else
				{
					UI.Instance.EnqueueMessage("Insufficient population to support new building, try building some housing");
				}
                /*/
            }
            if (InputManager.Instance.RightPressRelease() && buildingString != "LanderBase")
            {
                //if right click, no building selected, and close building selection menu
                UI.Instance.buildingSelected = "";
                UI.Instance.UIItemList[9].active = false;
                buildingPlacementTexture = null;
                tempBuilding = null;
                MenuController.Instance.UnloadContent(ConstructionMenu.Instance);
            }
        }

        void newTempBuilding(Vector2 p, ContentManager Content)
        {
            //gets the mouses position in the world and sets it in p
            //get the tile x and y position
            int x = (int)Math.Floor(p.X / 64);
            int y = (int)Math.Floor(p.Y / 64);
            //scale back up, p will now be in line with the tiles
            p = new Vector2(x * 64f, y * 64f);
            object[] po = new object[2];
            po[0] = Content;
            po[1] = p;
            try
            {
                //magic?
                tempBuilding = (Building)Activator.CreateInstance(Type.GetType("Remnants." + UI.Instance.buildingSelected), po);
                buildingPlacementTexture = tempBuilding.texture;
                placementMask = Color.Red;
            }
            catch (Exception)
            {
                throw;
            }
        }

        void spawnUnits(ContentManager Content)
        {
            var r = new Random();
            spawnIterations++;
            for (int i = 0; i < spawnIterations * UnitSpawnCount; i++)
            {
                int tblr = r.Next(4);
                int x = r.Next((map.xTiles - 1) * 64);
                int y = r.Next((map.yTiles - 1) * 64);
                Vector2 va;
                if (tblr == 0)
                {
                    va = new Vector2((float)x + 32f, 32f);
                }
                else if (tblr == 1)
                {
                    va = new Vector2(32f, (float)y + 32f);
                }
                else if (tblr == 2)
                {
                    va = new Vector2(((map.xTiles) * 64f) - 32f, (float)y + 32);
                }
                else
                {
                    va = new Vector2((float)x, ((map.yTiles) * 64f) - 32f);
                }
                //Vector2 va = new Vector2((float)x, (float)y);
                if (isPositionValid(va)) {
                    DefaultAlien testAlien = new DefaultAlien(Content);
                    //testAlien.position = Vector2.Zero;
                    testAlien.position = va;
                    enemyUnits.Add(testAlien);
                }
            }
            timePassed = 0;
        }

        public List<Entity> getNearbyEntities(Entity u, float range)
        {
            List<Entity> ret = new List<Entity>();
            //search entities first
            float dsq = range * range;
            foreach (Unit i in enemyUnits)
            {
                if (u != i && (u.position - i.position).LengthSquared() <= dsq)
                {
                    ret.Add(i);
                }
            }
            foreach (Building i in buildings)
            {
                if (u != i && (u.position - i.position).LengthSquared() <= dsq)
                {
                    ret.Add(i);
                }
            }
            return ret;
        }

        public List<Entity> getNearbyEnemies(Entity e, float range)
        {
            List<Entity> res = getNearbyEntities(e, range);
            List<Entity> ret = new List<Entity>();
            foreach (Entity ent in res)
            {
                if (ent.team != e.team)
                {
                    ret.Add(ent);
                }
            }
            return ret;
        }

        public Entity getNearestEnemy(Entity e, float maxRange)
        {
            List<Entity> toCheck = getNearbyEnemies(e, maxRange);
            float minDist = float.MaxValue;
            Entity ret = null;
            foreach (Entity ent in toCheck)
            {
                float dist = (ent.position - e.position).LengthSquared();
                if (dist < minDist)
                {
                    ret = ent;
                    minDist = dist;
                }
            }
            return ret;
        }

        public void UpdatePopulation(GameTime gameTime)
		{
			var deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			elapsedConsumptionTime += deltaT;
			if (elapsedConsumptionTime >= 1f)
			{
				int[] popConsumption = new int[8];
				popConsumption[(int)resources.FOOD] = (int)Math.Ceiling(LevelData.Instance.resourceList[(int)resources.POP] * 0.5);
				popConsumption[(int)resources.WATER] = (int)Math.Ceiling(LevelData.Instance.resourceList[(int)resources.POP] * 0.5);
				if (LevelData.Instance.checkResources(popConsumption))
				{
					// consume food and water
					LevelData.Instance.resourceList[(int)resources.FOOD] -= popConsumption[(int)resources.FOOD];
					LevelData.Instance.resourceList[(int)resources.WATER] -= popConsumption[(int)resources.WATER];

					// check for population growth
					if (LevelData.Instance.canPopGrow())
					{
						if (LevelData.Instance.checkResources(populationGrowthCost))
						{
							lastPopulationGrowth += elapsedConsumptionTime;
							//Console.Write("lastPopulationGrowth: " + lastPopulationGrowth + "\n");
							double prob = 1 - Math.Pow(Math.E,-(lastPopulationGrowth/meanGrowthTime)); // exponential distribution
							if (r.NextDouble() < prob)
							{
								// population grows
								UI.Instance.EnqueueMessage("Population grows!");
								LevelData.Instance.resourceList[(int)resources.POP] += 1;
								LevelData.Instance.resourceList[(int)resources.FOOD] -= populationGrowthCost[(int)resources.FOOD];
								LevelData.Instance.resourceList[(int)resources.WATER] -= populationGrowthCost[(int)resources.WATER];
								lastPopulationGrowth = 0f;
							}
						}
						else
						{
							//UI.Instance.EnqueueMessage("Population cannot grow");
						}
					}
				}
				else
				{
					UI.Instance.EnqueueMessage("Not enough food or water! Citizens may starve!");
					if (r.NextDouble() < 0.33)
					{
						UI.Instance.EnqueueMessage("A citizen has died!");
						LevelData.Instance.resourceList[(int)resources.POP] -= 1;
					}
				}
				elapsedConsumptionTime = 0f;
			}
		}

        internal Path reconPath(tileCoords tc)
        {
            tileCoords cur = tc;
            Path ret = new Path();
            Queue<tileCoords> q = new Queue<tileCoords>();
            while (cur.last != null)
            {
                //q.Enqueue(cur);
                ret.addNode(new Vector2(cur.x * 64 + 32, cur.y * 64 + 32));
                cur = cur.last;
            }
            /*while(q.Count > 0)
            {
                tileCoords t = q.Dequeue();
                
            }*/
            return ret;
        }

        internal class offset
        {
            public int x, y;
            public offset(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        internal offset[] neighbors = { new offset(-1, 0), new offset(1, 0), new offset(0, -1), new offset(0, 1) };

        public Path getPathToLocation(Vector2 start, Vector2 goal)
        {
            //Console.Out.WriteLine("Pathing to location...." + goal + " from " + start);
            Dictionary<int, tileCoords> closedSet = new Dictionary<int, tileCoords>();
            Dictionary<int, tileCoords> openSet = new Dictionary<int, tileCoords>();
            tileCoords s = new tileCoords(start);
            tileCoords g = new tileCoords(goal);
            openSet.Add(s.GetHashCode(), s);
            //set start defaults
            s.gCost = 0;
            s.hCost = s.getHCostTo(g);
            s.fCost = s.hCost;
            //now for actaul A*
            while (openSet.Count != 0)
            {
                //Console.Out.WriteLine("iteration!");
                tileCoords cur = null;
                foreach (tileCoords tc in openSet.Values)
                {
                    if (cur == null)
                    {
                        cur = tc;
                    }
                    else if (tc.fCost < cur.fCost)
                    {
                        cur = tc;
                    }
                }
                if (cur.GetHashCode() == g.GetHashCode())
                {
                    //Console.Out.WriteLine("PATH FOUND!");
                    return reconPath(cur);
                }
                //make tile closed
                openSet.Remove(cur.GetHashCode());
                closedSet.Add(cur.GetHashCode(), cur);
                //Console.Out.WriteLine("testing cur " + cur.x + ", " + cur.y);
                //add in neighbors
                for (int i = 0; i < neighbors.Length; i++)
                {
                    tileCoords t = new tileCoords(cur);
                    t.x += neighbors[i].x;
                    t.y += neighbors[i].y;
                    //Console.Out.WriteLine("testing "+ t.x + ", " + t.y);
                    if (!closedSet.ContainsKey(t.GetHashCode()) && t.x < map.tiles.Length && t.y < map.tiles[0].Length && t.x >= 0 && t.y >= 0 && map.tiles[t.x][t.y].canWalk)
                    {
                        float tgs = cur.gCost + 10;
                        bool cont = true;
                        if (!openSet.ContainsKey(t.GetHashCode()))
                        {
                            openSet.Add(t.GetHashCode(), t);
                        }
                        else
                        {
                            tileCoords n = openSet[t.GetHashCode()];
                            if (tgs <= n.gCost)
                            {
                                cont = false;
                            }
                            else
                                t = n;
                        }
                        if (cont)
                        {
                            t.last = cur;
                            t.gCost = tgs;
                            t.hCost = t.getHCostTo(g);
                            t.fCost = t.gCost + t.hCost;
                        }
                    }
                }
            }
            //Console.Out.WriteLine("Path not found!");
            return null;
        }

        internal bool isPositionValid(Vector2 position)
        {
            Tile t = map.GetTile(position);
            if (t == null || !t.canWalk)
                return false;
            return true;
        }

        public void EnableOrDisableBuilding(Vector2 p)
		{
			// if no building selected and m1 pressed
			if (UI.Instance.buildingSelected == "" && InputManager.Instance.LeftPressRelease())
			{
				// check each building to see if p is in that building's bounds
				foreach (Building building in buildings)
				{
					if (building.position.X < p.X && p.X < building.position.X + building.tilesWide*64)
					{
						if (building.position.Y < p.Y && p.Y < building.position.Y + building.tilesHigh*64)
						{
							if (building.isDisabled())
							{
								building.enable(this);
							}
							else
							{
								building.disable(this);
							}
						}
					}
				}
			}
		}

        void processLanding()
        {
            landed = true;
            for(int i = 0; i < 8; i++)
            {
                LevelData.Instance.resourceList[i] = tempBuilding.resourceStorage[i];
            }
            UI.Instance.buildingSelected = "";
        }

        private StorageDevice getStorageDevice()
        {
            IAsyncResult result;
            // Get a global folder.
            result = StorageDevice.BeginShowSelector(null, null);

            result.AsyncWaitHandle.WaitOne();
            StorageDevice device = StorageDevice.EndShowSelector(result);
            result.AsyncWaitHandle.Close();
            return device;
        }
        
        public void LoadGame(string filename)
        {
            device = getStorageDevice();

            // Open a storage container.
            IAsyncResult result = device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            // Check to see whether the save exists.
            if (!container.FileExists(filename))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return;
            }

            // Open the file.
            Stream stream = container.OpenFile(filename, FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(LevelData));

            LevelData data = (LevelData)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();
            // Dispose the container.
            container.Dispose();
            LevelData.SetLevelData(data);
            difficulty = LevelData.Instance.difficulty;
            landed = LevelData.Instance.landed;
        }

        public void SaveGame()
        {
            UI.Instance.EnqueueMessage("Saving Game");
            LevelData.Instance.landed = landed;
            LevelData.Instance.buildingList = new List<LevelData.buildingData>();
            foreach (Building b in buildings)
            {
                LevelData.Instance.buildingList.Add(new LevelData.buildingData(b));
            }
            Console.WriteLine("saving " + LevelData.Instance.buildingList.Count + "buildings");
            map.SaveTileData();

            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine(LevelData.Instance.resourceList[i].ToString());
                Console.WriteLine(LevelData.Instance.resourceLimits[i].ToString());
            }

            device = getStorageDevice();

            // Open a storage container.
            IAsyncResult result = device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            string filename = "savegame.sav";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(LevelData));
            serializer.Serialize(stream, LevelData.Instance);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
        }
        
    }
}
