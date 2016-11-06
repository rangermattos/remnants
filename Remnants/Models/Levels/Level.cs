using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Remnants
{
    public class Level
    {
        StorageDevice device;
        Map map;
        List<Building> buildings = new List<Building>();
        SpriteFont font = MenuController.Instance.font;

        public Level()
        {
            LevelData.Instance.SetLimits(500);
            map = new Map();
            Camera.Instance.cam.Position = LevelData.Instance.mapSize * 64 / 2;
        }

        //level constructer with loadgame
        public Level(string filename)
        {
            LoadGame(filename);
            map = new Map(LevelData.Instance.mapSize);
        }

        public void LoadContent(ContentManager Content)
        {
            map.LoadContent(Content);

            if(LevelData.Instance.buildingList.Count > 0)
            {
                LoadBuildings(Content);
            }
        }

        void LoadBuildings(ContentManager Content)
        {
            object[] po = new object[2];
            po[0] = Content;
            foreach (LevelData.buildingData b in LevelData.Instance.buildingList)
            {
                po[1] = b.Position;
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
                if (tempBuilding.Place(map))
                {
                    buildings.Add(tempBuilding);
                }
            }
        }

        public void UnloadContent(ContentManager Content)
        {
            //game.Content.Unload();
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            var deltaT = gameTime.ElapsedGameTime.TotalSeconds;

            LevelData.Instance.Update();
            UI.Instance.Update(gameTime);

            Vector2 p = Camera.Instance.cam.ScreenToWorld(InputManager.Instance.MousePosition);
            
            UpdateBuildings(gameTime, p);

            CheckBuilding(p, Content);

            //SetResources();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Camera.Instance.cam.GetViewMatrix());
            map.Draw(spriteBatch);
            foreach (Building b in buildings)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.End();

            UI.Instance.Draw(spriteBatch);
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
            }
        }

        public void CheckBuilding(Vector2 p, ContentManager Content)
        {
            string buildingString = UI.Instance.buildingSelected;
            if (buildingString != "" && InputManager.Instance.LeftPressRelease())
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
                Building tempBuilding;
                try
                {
                    //magic?
                    tempBuilding = (Building)Activator.CreateInstance(Type.GetType("Remnants." + buildingString), po);
                }
                catch (Exception)
                {
                    throw;
                }
                if (tempBuilding.Place(map))
                {
                    buildings.Add(tempBuilding);
                }
            }
            if (InputManager.Instance.RightPressRelease())
            {
                //if right click, no building selected, and close building selection menu
                UI.Instance.buildingSelected = "";
                UI.Instance.UIItemList[10].active = false;
                MenuController.Instance.UnloadContent(ConstructionMenu.Instance);
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

            XmlSerializer serializer = new XmlSerializer(typeof(LevelData));

            LevelData data = (LevelData)serializer.Deserialize(stream);
            // Close the file.
            stream.Close();
            // Dispose the container.
            container.Dispose();
            LevelData.Instance.SetLevelData(data);
        }

        public void SaveGame()
        {
            foreach(Building b in buildings)
            {
                LevelData.Instance.buildingList.Add(new LevelData.buildingData(b));
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
        
        /*
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
        */
    }
}
