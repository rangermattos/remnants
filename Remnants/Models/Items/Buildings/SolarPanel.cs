using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    class SolarPanel : Building
    {
        public SolarPanel(ContentManager Content, Vector2 pos) : base()
        {
            tilesWide = 2;
            tilesHigh = 1;
            Position = pos;
            //Position = Vector2.Zero;
            buildTime = 10f;
			deltas[(int)resources.ENERGY] = 10;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
			animated = true;
			animation = new Animation(Content, "buildings/solar_panel_spritesheet", 0.1f, 25, tilesHigh*64, tilesWide*64, true, true);
            LoadContent(Content);
        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("buildings/solarPanel");
            base.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override bool Place(Map map)
        {
            return base.Place(map);
        }
    }
}
