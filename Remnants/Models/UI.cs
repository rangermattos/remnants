using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Remnants
{
    class UI
    {
        public List<UIItem> UIItemList = new List<UIItem>();
        Vector2 topLeft = new Vector2();
        SpriteFont font;
        public string buildingSelected { get; set; }
        private static UI instance;

        private UI() { }
        private UI(SpriteFont font, Vector2 position, ContentManager Content, Vector2 viewport)
        {
            this.font = font;
            buildingSelected = "";
            topLeft = Vector2.Zero;

            //UIBar
            Vector2 textureScale = new Vector2(Camera.Instance.cam.Origin.X * 2 * Camera.Instance.viewportScale.Scale.X, 32 * Camera.Instance.viewportScale.Scale.Y);
            AddItem(topLeft, Vector2.Zero, textureScale, Content.Load<Texture2D>("grayDot"), true);
            //construct
			AddItem(topLeft, position, Content.Load<Texture2D>("icons/hammer"), (bool active) => { ConstructionMenus(active); Console.Write("construction icon clicked:" + active + "\n"); return 0; });
			UIItemList[1].active = true;
            //food
            AddItem(LevelData.Instance.resourceList[0], LevelData.Instance.resourceLimits[0], font, topLeft, position, Content.Load<Texture2D>("icons/food_icon"), () => { OnClickConstruct(); return 0; });
            //water
            AddItem(LevelData.Instance.resourceList[1], LevelData.Instance.resourceLimits[0], font, topLeft, position, Content.Load<Texture2D>("icons/water_icon"), () => { OnClickConstruct(); return 0; });
            //energy
            AddItem(LevelData.Instance.resourceList[2], LevelData.Instance.resourceLimits[0], font, topLeft, position, Content.Load<Texture2D>("icons/energy_icon"), () => { OnClickConstruct(); return 0; });
            //antimatter
            AddItem(LevelData.Instance.resourceList[3], LevelData.Instance.resourceLimits[0], font, topLeft, position, Content.Load<Texture2D>("icons/antimatter_icon"), () => { OnClickConstruct(); return 0; });
            //nuclear
            AddItem(LevelData.Instance.resourceList[4], LevelData.Instance.resourceLimits[0], font, topLeft, position, Content.Load<Texture2D>("icons/nuclear_icon"), () => { OnClickConstruct(); return 0; });
            //wood
            AddItem(LevelData.Instance.resourceList[5], LevelData.Instance.resourceLimits[0], font, topLeft, position, Content.Load<Texture2D>("icons/wood_icon"), () => { OnClickConstruct(); return 0; });
            //Metal
            AddItem(LevelData.Instance.resourceList[6], LevelData.Instance.resourceLimits[0], font, topLeft, position, Content.Load<Texture2D>("icons/metal_icon"), () => { OnClickConstruct(); return 0; });
            //Population
            AddItem(LevelData.Instance.resourceList[7], LevelData.Instance.resourceLimits[0], font, topLeft, position, Content.Load<Texture2D>("icons/pop_icon"), () => { OnClickConstruct(); return 0; });

            //construction popupmenu
            textureScale = new Vector2(ConstructionMenu.Instance.maxWidth, ConstructionMenu.Instance.totHeight);
            AddItem(topLeft, new Vector2(0, ConstructionMenu.Instance.center.Y - (ConstructionMenu.Instance.totHeight / 2)), textureScale, Content.Load<Texture2D>("grayDot"), false);
            
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

        public static void Create(SpriteFont font, Vector2 position, ContentManager Content, Vector2 viewport)
        {
            if (instance != null)
            {
                throw new Exception("Object already created");
            }
            instance = new UI(font, position, Content, viewport);
        }

        public void Update(GameTime gameTime, MouseState state, MouseState prevState)
        {
            int j = 0;
            int k = 0;
            foreach (UIItem i in UIItemList)
            {
                //if (i != UIItemList[0] && i != UIItemList[1])
                if(k > 1 && k < 10)
                {
                    i.Update(state, prevState, LevelData.Instance.resourceList[j], LevelData.Instance.resourceLimits[j]);
                    j++;
                }
                else
                {
                    i.Update(state, prevState);
                }
                k++;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (UIItem i in UIItemList)
            {
                i.Draw(spriteBatch);
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

        void AddItem(int v, int vl, SpriteFont font, Vector2 tl, Vector2 position, Texture2D texture, System.Func<int> UIItemAction)
        {
            float scale = 0.275f;
            UIItem item = new UIItem(scale, v, vl, font, tl, position, texture, UIItemAction);
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
            UIItemList[0].position = tempVect;

            tempVect = new Vector2(0, viewport.Y - 32);
            UIItemList[1].position = tempVect;
            float scale = Camera.Instance.viewportScale.Scale.X;
            //32 for icon width, 1 for spacing between, 128 for readout text width
            // - (32 * scale + 1 + 128 * scale) * 7
            float x = (Camera.Instance.cam.Origin.X * 2) - (32 * scale + 1 + 128 * scale) * 7;

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
                    x += (32 * scale + 1 + 128 * scale);
                }
                k++;
            }

            tempVect = new Vector2(0, 0);
            UIItemList[9].position = tempVect;
            UIItemList[9].valuePosition = new Vector2(34, UIItemList[9].valuePosition.Y);
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
                MenuController.Instance.UnloadContent();
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
            MenuController.Instance.UnloadContent();
            buildingSelected = "";
        }
    }
}
