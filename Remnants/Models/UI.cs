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
            AddItem(topLeft, Vector2.Zero, Content.Load<Texture2D>("UIBar1280"), () => { OnClickConstruct(); return 0; });
            //construct
			AddItem(topLeft, position, Content.Load<Texture2D>("icons/hammer"), (bool active) => { ConstructionMenus(active); Console.Write("construction icon clicked:" + active + "\n"); return 0; });
			UIItemList[1].active = true;
            //food
            AddItem(LevelData.Instance.resourceList[0], font, topLeft, position, Content.Load<Texture2D>("icons/food_icon"), () => { OnClickConstruct(); return 0; });
            //water
            AddItem(LevelData.Instance.resourceList[1], font, topLeft, position, Content.Load<Texture2D>("icons/water_icon"), () => { OnClickConstruct(); return 0; });
            //energy
            AddItem(LevelData.Instance.resourceList[2], font, topLeft, position, Content.Load<Texture2D>("icons/energy_icon"), () => { OnClickConstruct(); return 0; });
            //antimatter
            AddItem(LevelData.Instance.resourceList[3], font, topLeft, position, Content.Load<Texture2D>("icons/antimatter_icon"), () => { OnClickConstruct(); return 0; });
            //nuclear
            AddItem(LevelData.Instance.resourceList[4], font, topLeft, position, Content.Load<Texture2D>("icons/nuclear_icon"), () => { OnClickConstruct(); return 0; });
            //wood
            AddItem(LevelData.Instance.resourceList[5], font, topLeft, position, Content.Load<Texture2D>("icons/wood_icon"), () => { OnClickConstruct(); return 0; });
            //Metal
            AddItem(LevelData.Instance.resourceList[6], font, topLeft, position, Content.Load<Texture2D>("icons/metal_icon"), () => { OnClickConstruct(); return 0; });
            //Population
            AddItem(LevelData.Instance.resourceList[7], font, topLeft, position, Content.Load<Texture2D>("icons/pop_icon"), () => { OnClickConstruct(); return 0; });

            //construction popupmenu
            AddItem(topLeft, Content.Load<Texture2D>("grayDot"), (bool active) => { Console.Write("Clicking menu\n"); OnClickConstructMenu(active); Console.Write("Menu clicked\n"); return 0; });
            
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
                    i.Update(state, prevState, LevelData.Instance.resourceList[j]);
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

        void AddItem(Vector2 tl, Vector2 position, Texture2D texture, System.Func<bool, int> UIItemAction)
        {
            UIItem item = new UIItem(tl, position, texture, UIItemAction);
            UIItemList.Add(item);
        }

        void AddItem(int v, SpriteFont font, Vector2 tl, Vector2 position, Texture2D texture, System.Func<int> UIItemAction)
        {
            float scale = 0.3f;
            UIItem item = new UIItem(scale, v, font, tl, position, texture, UIItemAction);
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

            float x = 1280 - (32 + 1 + 64) * 7;

            int k = 0;
            foreach(UIItem i in UIItemList)
            {
                //if (i != UIItemList[0] && i != UIItemList[1] && i != UIItemList[9] && i != UIItemList[10])
                if (k > 1 && k < 9)
                {
                    i.position = new Vector2(x, 0);
                    i.valuePosition = new Vector2(x + 34, i.valuePosition.Y);
                    x += 32 + 1 + 64;
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
                MenuController.Instance.UnloadContent();
                Console.WriteLine("buildingSelected: " + buildingSelected);
            }
            //buildingSelected = "";
        }
    }
}
