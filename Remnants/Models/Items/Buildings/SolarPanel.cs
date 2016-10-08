using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    class SolarPanel : Building
    {
        public SolarPanel(ContentManager Content, Vector2 pos)
        {
            tilesWide = 2;
            tilesHigh = 1;
            Position = pos;
            buildTime = 10f;
            deltaPower = 20;
            metalCost = 100;
            powerCost = 100;
            LoadContent(Content);
        }
        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("solarPanel");
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
    }
}
