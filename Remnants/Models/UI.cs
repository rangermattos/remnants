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
        public string buildingSelected { get; set; }

        public UI(SpriteFont font, Vector2 position, ContentManager Content, Vector2 viewport, List<int> resourceList)
        {
            buildingSelected = "";
            topLeft = Vector2.Zero;
            //UIBar
            AddItem(topLeft, Vector2.Zero, Content.Load<Texture2D>("UIBar1280"), () => { OnClickConstruct(); return 0; });
            //construct
            AddItem(topLeft, position, Content.Load<Texture2D>("construct"), (bool active) => { ConstructionMenu(active); return 0; });
            //food
            AddItem(resourceList[0], font, topLeft, position, Content.Load<Texture2D>("food_icon"), () => { OnClickConstruct(); return 0; });
            //water
            AddItem(resourceList[1], font, topLeft, position, Content.Load<Texture2D>("water_icon"), () => { OnClickConstruct(); return 0; });
            //energy
            AddItem(resourceList[2], font, topLeft, position, Content.Load<Texture2D>("energy_icon"), () => { OnClickConstruct(); return 0; });
            //antimatter
            AddItem(resourceList[3], font, topLeft, position, Content.Load<Texture2D>("antimatter_icon"), () => { OnClickConstruct(); return 0; });
            //nuclear
            AddItem(resourceList[4], font, topLeft, position, Content.Load<Texture2D>("nuclear_icon"), () => { OnClickConstruct(); return 0; });
            //wood
            AddItem(resourceList[5], font, topLeft, position, Content.Load<Texture2D>("wood_icon"), () => { OnClickConstruct(); return 0; });
            //Metal
            AddItem(resourceList[6], font, topLeft, position, Content.Load<Texture2D>("metal_icon"), () => { OnClickConstruct(); return 0; });
            //Population
            AddItem(resourceList[7], font, topLeft, position, Content.Load<Texture2D>("pop_icon"), () => { OnClickConstruct(); return 0; });

            //construction popupmenu
            
            List<string> l = new List<string>();
            l.Add("Solar Panel");
            l.Add("Shock Trap");
			l.Add("Wind Turbine");
			l.Add("Small Battery Facility");
			l.Add("Medium Battery Facility");
			l.Add("Large Battery Facility");
			l.Add("Small House");
			l.Add("Medium House");
			l.Add("Large House");
            position = new Vector2(0, viewport.Y - 32);
            AddItem(l, font, topLeft, position, Content.Load<Texture2D>("grayDot"), (bool active) => { OnClickConstructMenu(active); return 0; });
            
            SetItemPositions(viewport, resourceList);
        }

        public void Update(GameTime gameTime, MouseState state, MouseState prevState, List<int> resourceList)
        {
            int j = 0;
            int k = 0;
            foreach (UIItem i in UIItemList)
            {
                //if (i != UIItemList[0] && i != UIItemList[1])
                if(k > 1 && k < 10)
                {
                    i.Update(state, prevState, resourceList[j]);
                    j++;
                }
                else
                {
                    i.Update(state, prevState, this);
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
        
        void AddItem(List<string> stringList, SpriteFont font, Vector2 tl, Vector2 position, Texture2D texture, System.Func<bool, int> UIItemAction)
        {
            float scale = 0.3f;
            UIItem item = new UIItem(scale, stringList, font, tl, position, texture, UIItemAction);
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
            UIItemList[10].active = active;
        }

        void ConstructionMenu(bool active)
        {
            UIItemList[10].active = active;
            buildingSelected = "";
        }
    }
}
