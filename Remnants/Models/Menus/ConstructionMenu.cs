using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Remnants
{
    class ConstructionMenu : Menu
    {
        private static ConstructionMenu instance;
        //public static float totHeight { get; set; }
        //public static float maxWidth { get; set; }
        public float totHeight { get; set; }
        public float maxWidth { get; set; }
        public Vector2 center;

        private ConstructionMenu()
        {
            scale = 0.3f;
			List<string> l = new List<string>();
			l.Add("Shock Trap");
            l.Add("Solar Panel");
            l.Add("Wind Turbine");
            l.Add("Small Battery Facility");
            l.Add("Medium Battery Facility");
            l.Add("Large Battery Facility");
            l.Add("Small House");
            l.Add("Medium House");
            l.Add("Large House");
            l.Add("Greenhouse");
            l.Add("Water Purification");
            l.Add("Mine");
            l.Add("Granary");
            l.Add("Water Tower");
            l.Add("Warehouse");
			l.Add("Antimatter Containment Unit");
			l.Add("Antimatter Creation Lab");
			l.Add("Antimatter Generator");
			l.Add("Nuclear Mine");
			l.Add("Nuclear Plant");
			l.Add("Nuclear Storage");

            float ftotHeight = 0f;
            float fmaxWidth = 0f;

            foreach (string st in l)
            {
                //Vector2 tempVect = Vector2.Transform((MenuController.Instance.font.MeasureString(st) * scale), Camera.Instance.viewportScale);
                Vector2 tempVect = MenuController.Instance.font.MeasureString(st) * scale;
                ftotHeight += (tempVect.Y);
                if (tempVect.X > fmaxWidth)
                {
                    fmaxWidth = tempVect.X;
                }
            }
            fmaxWidth += 4f;

            totHeight = ftotHeight;
            maxWidth = fmaxWidth / Camera.Instance.viewportScale.Scale.X;

            //var v = Vector2.Transform(Vector2.Zero, Matrix.Invert(Camera.Instance.cam.GetViewMatrix()));
            var v = new Vector2(0, (Camera.Instance.cam.Origin.Y * 2) - 32);
            center = new Vector2(maxWidth / 2, v.Y - (totHeight / 2));
            //center = Vector2.Transform(center, Camera.Instance.viewportScale);
            object[] po = new object[2];
            po[0] = MenuController.Instance.cont;
            po[1] = Vector2.Zero;
            foreach (string s in l)
            {
                //create an instance of the building represented by the menu item and 
                //store it in this menu item.
                //this is used to check if the building cost 
                //is greater than the players current resource amounts
                //this building is never updated or drawn, or included in any list with the levels active buildings
                Building tempBuilding;
                try
                {
                    //magic?
                    tempBuilding = (Building)Activator.CreateInstance(Type.GetType("Remnants." + s.Replace(" ", "")), po);
                }
                catch (Exception)
                {
                    throw;
                }

                itemCount += AddItem(tempBuilding, totHeight, scale, s, MenuController.Instance.font, center, () => { /*MenuController.Instance.UnloadContent();*/ return s.Replace(" ", ""); });
            }
            SetPositions(center, 0);

            foreach (MenuItem i in menuItemList)
            {
                //i.origin = Vector2.Zero;
            }

        }

        public static ConstructionMenu Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ConstructionMenu();
                }
                return instance;
            }
        }

    }
}
