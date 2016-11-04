using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

namespace Remnants
{
    public class Level
    {
        [Serializable]
        public struct LevelDetails
        {
            public Map map;
            //public List<Building> buildings;
            public int energy;
            public int metal;
            public int pop;
            public int food;
            public int water;
            public int antimatter;
            public int wood;
            public int nuclear;
            public List<int> resourceList;

            public LevelDetails(Map map, List<Building> buildings, int energy, int metal, int pop, int food, int water, int antimatter, int wood, int nuclear, List<int> resourceList)
            {
                this.map = map;
                //this.buildings = buildings;
                this.energy = energy;
                this.metal = metal;
                this.pop = pop;
                this.food = food;
                this.water = water;
                this.antimatter = antimatter;
                this.wood = wood;
                this.nuclear = nuclear;
                this.resourceList = resourceList;
            }
        }

        [NonSerialized]
        StorageDevice device;
        Map map;
        List<Building> buildings = new List<Building>();
        int energy;
        int metal;
        int pop;
        int food;
        int water;
        int antimatter;
        int wood;
        int nuclear;
        List<int> resourceList = new List<int>();

        //UI ui;
        public Vector2 mapSize;
        KeyboardState prevKeyState;
        MouseState prevMouseState;
        MouseState mouseState;
        Vector2 mousePosition;
        SpriteFont font;

        public Level(SpriteFont f)
        {
            font = f;
            mapSize = new Vector2(5760, 3240);
            map = new Map(mapSize);
            food = 10000;
            water = 10000;
            energy = 10000;
            antimatter = 10000;
            nuclear = 10000;
            wood = 10000;
            metal = 10000;
            pop = 10000;
            
            resourceList.Add(food);
            resourceList.Add(water);
            resourceList.Add(energy);
            resourceList.Add(antimatter);
            resourceList.Add(nuclear);
            resourceList.Add(wood);
            resourceList.Add(metal);
            resourceList.Add(pop);
            
        }

        //level constructer with loadgame
        public Level(SpriteFont f, string filename)
        {
            font = f;
            mapSize = new Vector2(5760, 3240);
            LoadGame(filename);
        }

        public void LoadContent(Game1 game, Matrix vm, Vector2 vpDimensions)
        {
            map.LoadContent(game.Content);
            UI.Create(font, Vector2.Transform(Vector2.Zero, Matrix.Invert(vm)), game.Content, vpDimensions, resourceList);
        }

        public void UnloadContent(Game1 game)
        {
            game.Content.Unload();
        }

        public void Update(GameTime gameTime, ContentManager Content, KeyboardState keyboardState)
        {
            mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);
            var deltaT = gameTime.ElapsedGameTime.TotalSeconds;
            var vm = Camera.Instance.cam.GetViewMatrix();

            UI.Instance.Update(gameTime, mouseState, prevMouseState, resourceList);

            Vector2 p = Camera.Instance.cam.ScreenToWorld(mousePosition);
            
            UpdateBuildings(gameTime, p);

            CheckBuilding(p, Content);

            SetResources();
            
            prevKeyState = keyboardState;
            prevMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(transformMatrix: Camera.Instance.cam.GetViewMatrix());
            map.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            foreach (Building b in buildings)
            {
                b.Draw(spriteBatch);
            }
            UI.Instance.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void UpdateBuildings(GameTime gameTime, Vector2 p)
        {
            foreach (Building b in buildings)
            {
                if (b.GetType().Name == "ShockTrap")
                {
                    b.Update(gameTime, p);
                }
                else
                {
                    b.Update(gameTime);
                }
                if (b.foodChange != 0)
                {
                    food += b.foodChange;
                    b.foodChange = 0;
                }
                if (b.waterChange != 0)
                {
                    water += b.waterChange;
                    b.waterChange = 0;
                }
                if (b.energyChange != 0)
                {
                    energy += b.energyChange;
                    b.energyChange = 0;
                }
                if (b.woodChange != 0)
                {
                    wood += b.woodChange;
                    b.woodChange = 0;
                }
                if (b.metalChange != 0)
                {
                    metal += b.metalChange;
                    b.metalChange = 0;
                }
            }
        }

        public void CheckBuilding(Vector2 p, ContentManager Content)
        {
            string buildingString = UI.Instance.buildingSelected;
            if (buildingString != ""
               && mouseState.LeftButton == ButtonState.Released
               && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine(buildingString);
                //gets the mouses position in the world and sets it in p
                //get the tile x and y position
                int x = (int)Math.Floor(p.X / 64);
                int y = (int)Math.Floor(p.Y / 64);
                //scale back up, p will now be in line with the tiles
                p = new Vector2(x * 64f, y * 64);

                if (buildingString == "SolarPanel")
                {
                    var tempBuilding = new SolarPanel(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new SolarPanel(Content, p));
                    }
                }
                else if (buildingString == "ShockTrap")
                {
                    var tempBuilding = new ShockTrap(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new ShockTrap(Content, p));
                    }
                }
                else if (buildingString == "WindTurbine")
                {
                    var tempBuilding = new WindTurbine(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new WindTurbine(Content, p));
                    }
                }
                else if (buildingString == "SmallBatteryFacility")
                {
                    var tempBuilding = new BatterySmall(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new BatterySmall(Content, p));
                    }
                }
                else if (buildingString == "MediumBatteryFacility")
                {
                    var tempBuilding = new BatteryMedium(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new BatteryMedium(Content, p));
                    }
                }
                else if (buildingString == "LargeBatteryFacility")
                {
                    var tempBuilding = new BatteryLarge(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new BatteryLarge(Content, p));
                    }
                }
                else if (buildingString == "SmallHouse")
                {
                    var tempBuilding = new HouseSmall(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new HouseSmall(Content, p));
                    }
                }
                else if (buildingString == "MediumHouse")
                {
                    var tempBuilding = new HouseMedium(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new HouseMedium(Content, p));
                    }
                }
                else if (buildingString == "LargeHouse")
                {
                    var tempBuilding = new HouseLarge(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new HouseLarge(Content, p));
                    }
                }
                else if (buildingString == "Greenhouse")
                {
                    var tempBuilding = new Greenhouse(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new Greenhouse(Content, p));
                    }
                }
                else if (buildingString == "WaterPurification")
                {
                    var tempBuilding = new WaterPurificationFacility(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new WaterPurificationFacility(Content, p));
                    }
                }
                else if (buildingString == "Mine")
                {
                    var tempBuilding = new Mine(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new Mine(Content, p));
                    }
                }
                else if (buildingString == "Granary")
                {
                    var tempBuilding = new Granary(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new Granary(Content, p));
                    }
                }
                else if (buildingString == "WaterTower")
                {
                    var tempBuilding = new WaterTower(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new WaterTower(Content, p));
                    }
                }
                else if (buildingString == "Warehouse")
                {
                    var tempBuilding = new Warehouse(Content, p);
                    if (tempBuilding.Place(map))
                    {
                        buildings.Add(new Warehouse(Content, p));
                    }
                }
            }
            else if (buildingString != ""
               && mouseState.RightButton == ButtonState.Released
               && prevMouseState.RightButton == ButtonState.Pressed)
            {
                UI.Instance.buildingSelected = "";
            }
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
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

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

            XmlSerializer serializer = new XmlSerializer(typeof(LevelDetails));

            LevelDetails data = (LevelDetails)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();
            // Dispose the container.
            container.Dispose();

            UnpackLevelDetails(data);
        }

        public void UnpackLevelDetails(LevelDetails data)
        {
            map = data.map;
            //buildings = data.buildings;
            energy = data.energy;
            metal = data.metal;
            pop = data.pop;
            food = data.food;
            water = data.water;
            antimatter = data.antimatter;
            wood = data.wood;
            nuclear = data.nuclear;
            resourceList = data.resourceList;
        }

        public void SaveGame()
        {
            LevelDetails sg = new LevelDetails(map, buildings, energy, metal, pop, food, water, antimatter, wood, nuclear, resourceList);

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
            XmlSerializer serializer = new XmlSerializer(typeof(LevelDetails));
            serializer.Serialize(stream, sg);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
        }

        void SetResources()
        {
            resourceList[0] = food;
            resourceList[1] = water;
            resourceList[2] = energy;
            resourceList[3] = antimatter;
            resourceList[4] = nuclear;
            resourceList[5] = wood;
            resourceList[6] = metal;
            resourceList[7] = pop;
        }
    }
}
