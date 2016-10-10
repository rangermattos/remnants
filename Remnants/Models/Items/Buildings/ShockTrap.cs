using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    class ShockTrap : Building
    {
        public Texture2D halfCircle;
        public Texture2D lightningSegment;
        LightningBolt lb;

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
            lb = new LightningBolt(Position + new Vector2(texture.Width / 2, texture.Height / 2), Position + new Vector2(200, 200), Color.LightCyan);
        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ShockTrap");
            halfCircle = Content.Load<Texture2D>("glowLine3");
            lightningSegment = Content.Load<Texture2D>("glowLine2");
            base.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
            lb.Update();
            if(lb.Alpha <= 0)
            {
                lb = new LightningBolt(Position + new Vector2(texture.Width / 2, texture.Height / 2), Position + new Vector2(200, 200), Color.LightCyan);
            }
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            lb.Draw(spriteBatch, halfCircle, lightningSegment);
        }

        public override void Place()
        {
            base.Place();
        }
    }
}
