using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    class ShockTrap : Building
    {
        public ShockTrap(ContentManager Content, Vector2 pos)
        {
            tilesWide = 1;
            tilesHigh = 1;
            Position = pos;
            buildTime = 10f;
            deltaEnergy = -10;
            metalCost = 100;
            energyCost = 100;
            LoadContent(Content);
        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ShockTrap");
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

        public override void Place()
        {
            base.Place();
        }
    }
}
