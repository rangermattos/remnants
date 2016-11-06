using Microsoft.Xna.Framework;
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
            l.Add("Solar Panel");
            l.Add("Shock Trap");
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
            foreach (string s in l)
            {
                itemCount += AddItem(scale, s, MenuController.Instance.font, center, () => { /*MenuController.Instance.UnloadContent();*/ return s.Replace(" ", ""); });
            }
            SetPositions(center, 0);

            foreach(MenuItem i in menuItemList)
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
        /*
        public override string Update(MouseState state, Game1 game, MenuController mc)
        {
            string s = "";
            var v = new Vector2(state.X, state.Y);
            foreach (MenuItem item in menuItemList)
            {
                s = item.Update(state, game, mc);
                if(s != "")
                {
                    return s;
                }
            }
            return s;
        }
        */
    }
}
