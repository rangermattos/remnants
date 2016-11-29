using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Remnants
{
    class UI
    {
        public List<UIItem> UIItemList = new List<UIItem>();
        public string buildingSelected { get; set; }
        public bool isActive;
        Vector2 topLeft = new Vector2();
        SpriteFont font;
        private static UI instance;

        private UI() { }
        private UI(ContentManager Content)
        {
            Vector2 position = Vector2.Transform(Vector2.Zero, Matrix.Invert(Camera.Instance.cam.GetViewMatrix()));
            Vector2 viewport = Camera.Instance.cam.Origin * 2;
            this.font = MenuController.Instance.font;
            buildingSelected = "";
            topLeft = Vector2.Zero;
            isActive = false;

            //UIBar
            Vector2 textureScale = Vector2.Transform(new Vector2(Camera.Instance.cam.Origin.X * 2, 32), Camera.Instance.viewportScale);
            AddItem(topLeft, Vector2.Zero, textureScale, Content.Load<Texture2D>("grayDot"), true);
            //construct
			AddItem(topLeft, position, Content.Load<Texture2D>("icons/hammer"), (bool active) => { ConstructionMenus(active); Console.Write("construction icon clicked:" + active + "\n"); return 0; });
			UIItemList[1].active = true;
            //food
			AddItem(LevelData.Instance.resourceList[(int)resources.FOOD], LevelData.Instance.resourceLimits[0], topLeft, position, Content.Load<Texture2D>("icons/food_icon"), () => { OnClickConstruct(); return 0; });
            //water
			AddItem(LevelData.Instance.resourceList[(int)resources.WATER], LevelData.Instance.resourceLimits[0], topLeft, position, Content.Load<Texture2D>("icons/water_icon"), () => { OnClickConstruct(); return 0; });
            //energy
			AddItem(LevelData.Instance.resourceList[(int)resources.ENERGY], LevelData.Instance.resourceLimits[0], topLeft, position, Content.Load<Texture2D>("icons/energy_icon"), () => { OnClickConstruct(); return 0; });
            //antimatter
			AddItem(LevelData.Instance.resourceList[(int)resources.ANTIMATTER], LevelData.Instance.resourceLimits[0], topLeft, position, Content.Load<Texture2D>("icons/antimatter_icon"), () => { OnClickConstruct(); return 0; });
            //nuclear
			AddItem(LevelData.Instance.resourceList[(int)resources.NUCLEAR], LevelData.Instance.resourceLimits[0], topLeft, position, Content.Load<Texture2D>("icons/nuclear_icon"), () => { OnClickConstruct(); return 0; });
            //wood
			AddItem(LevelData.Instance.resourceList[(int)resources.WOOD], LevelData.Instance.resourceLimits[0], topLeft, position, Content.Load<Texture2D>("icons/wood_icon"), () => { OnClickConstruct(); return 0; });
            //Metal
			AddItem(LevelData.Instance.resourceList[(int)resources.METAL], LevelData.Instance.resourceLimits[0], topLeft, position, Content.Load<Texture2D>("icons/metal_icon"), () => { OnClickConstruct(); return 0; });
            //Population
			AddItem(LevelData.Instance.resourceList[(int)resources.POP], LevelData.Instance.resourceLimits[0], topLeft, position, Content.Load<Texture2D>("icons/pop_icon"), () => { OnClickConstruct(); return 0; });

            //construction popupmenu
            Vector2 temp = Vector2.Transform(new Vector2(0, ConstructionMenu.Instance.center.Y - (ConstructionMenu.Instance.totHeight / 2)), Camera.Instance.viewportScale);
            //Vector2 temp = new Vector2(0, ConstructionMenu.Instance.center.Y - (ConstructionMenu.Instance.totHeight / 2));
            textureScale = Vector2.Transform(new Vector2(ConstructionMenu.Instance.maxWidth, ConstructionMenu.Instance.totHeight), Camera.Instance.viewportScale);
            AddItem(topLeft, temp, textureScale, Content.Load<Texture2D>("grayDot"), false);

			// Message Queue
			UIItemList.Add(new MessageQueue(new Vector2(128, 128), new Vector2(128, 128), Vector2.Zero, Content.Load<Texture2D>("grayDot"), 5f));
            
            SetItemPositions(viewport, LevelData.Instance.resourceList);
        }

        public static UI Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("Object not created");
                }
                return instance;
            }
        }

        public static void Create(ContentManager Content)
        {
            if (instance != null)
            {
                return;
                //throw new Exception("Object already created");
            }
            instance = new UI(Content);
        }

        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                int j = 0;
                int k = 0;
                foreach (UIItem i in UIItemList)
                {
                    //if (i != UIItemList[0] && i != UIItemList[1])
                    if (k > 1 && k < 10)
                    {
                        i.Update(LevelData.Instance.resourceList[j], LevelData.Instance.resourceLimits[j]);
                        j++;
                    }
					else if (k == 11) // message queue
					{
						i.Update(gameTime);
					}
					else
                    {
                        i.Update();
                    }
                    k++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Begin();
                foreach (UIItem i in UIItemList)
                {
                    i.Draw(spriteBatch);
                }
                spriteBatch.End();
            }
        }

        void AddItem(Vector2 tl, Vector2 position, Texture2D texture, System.Func<int> UIItemAction)
        {
            UIItem item = new UIItem(tl, position, texture, UIItemAction);
            UIItemList.Add(item);
        }

        void AddItem(Vector2 tl, Vector2 position, Vector2 textureScale, Texture2D texture, bool active)
        {
            UIItem item = new UIItem(tl, position, textureScale, texture, active);
            UIItemList.Add(item);
        }

        void AddItem(Vector2 tl, Vector2 position, Texture2D texture, System.Func<bool, int> UIItemAction)
        {
            UIItem item = new UIItem(tl, position, texture, UIItemAction);
            UIItemList.Add(item);
        }

        void AddItem(int v, int vl, Vector2 tl, Vector2 position, Texture2D texture, System.Func<int> UIItemAction)
        {
            float scale = 0.275f;
            UIItem item = new UIItem(scale, v, vl, tl, position, texture, UIItemAction);
            UIItemList.Add(item);
        }
        
        void AddItem(Vector2 tl, Texture2D texture, System.Func<bool, int> UIItemAction)
        {
            UIItem item = new UIItem(tl, texture, UIItemAction);
            UIItemList.Add(item);
        }

        void SetItemPositions(Vector2 viewport, List<int> resourceList)
        {
            Vector2 tempVect = new Vector2(0, 0);
            UIItemList[0].position = Vector2.Transform(tempVect, Camera.Instance.viewportScale);
            tempVect = new Vector2(0, viewport.Y - 32);
            UIItemList[1].position = Vector2.Transform(tempVect, Camera.Instance.viewportScale);
            float scale = Camera.Instance.viewportScale.Scale.X;
            //32 for icon width, 1 for spacing between, 128 for readout text width
            // - (32 * scale + 1 + 128 * scale) * 7
            float x = (32 + 1 + 96);

            int k = 0;
            /*/
            for(int i = UIItemList.Count; i >= 0; i--)
            {
                //if (i != UIItemList[0] && i != UIItemList[1] && i != UIItemList[9] && i != UIItemList[10])
                if (k > 1 && k < 9)
                {
                    UIItemList[i].position = new Vector2(x, 0);
                    UIItemList[i].valuePosition = new Vector2(x + 34, UIItemList[i].valuePosition.Y);
                    x -= (32 * scale + 1 + 128 * scale);
                }
                k++;
            }
            /*/
            foreach (UIItem i in UIItemList)
            {
                //if (i != UIItemList[0] && i != UIItemList[1] && i != UIItemList[9] && i != UIItemList[10])
                if (k > 1 && k < 9)
                {
                    i.position = new Vector2(x, 0);
                    i.valuePosition = new Vector2(x + 34, i.valuePosition.Y);
                    i.position = Vector2.Transform(i.position, Camera.Instance.viewportScale);
                    i.valuePosition = Vector2.Transform(i.valuePosition, Camera.Instance.viewportScale);
                    //i.valueLimitPosition = new Vector2((i.valuePosition.X + (font.MeasureString(i.value.ToString())).X) * scale, i.valuePosition.Y);
                    x += (32 + 1 + 96);
                }
                k++;
            }

            tempVect = new Vector2(0, 0);
            UIItemList[9].position = Vector2.Transform(tempVect, Camera.Instance.viewportScale);
            tempVect = new Vector2(34, UIItemList[9].valuePosition.Y);
            UIItemList[9].valuePosition = Vector2.Transform(tempVect, Camera.Instance.viewportScale);

            //UIItemList[10].position = Vector2.Transform()
        }

        void OnClickConstruct()
        {

        }

        void OnClickConstructMenu(bool active)
        {
			Console.Write("Making construction UI active: " + active + "\n");
			
            UIItemList[10].active = !UIItemList[10].active;
            if (!UIItemList[10].active)
            {
                MenuController.Instance.UnloadContent(ConstructionMenu.Instance);
            }
        }

        void ConstructionMenus(bool active)
        {
            UIItemList[10].active = !UIItemList[10].active;
            if (UIItemList[10].active)
            {
                //Menu m = ConstructionMenu.Instance;
                MenuController.Instance.SetMenu(ConstructionMenu.Instance);
                //MenuController.Instance.SetMenu(new MainMenu(font));
            }
            else
            {
                BuildingSelectorDeactivate();
            }
        }

        public void BuildingSelectorDeactivate()
        {
            MenuController.Instance.UnloadContent(ConstructionMenu.Instance);
            buildingSelected = "";
        }

		public void EnqueueMessage(string msg)
		{
			((MessageQueue)UIItemList[11]).addMessage(msg);
		}

        public bool MouseOverUI()
        {
            if (UIItemList[0].IsItemHovered() || UIItemList[1].IsItemHovered() || UIItemList[10].IsItemHovered())
                return true;
            
            return false;
        }
    }
}
