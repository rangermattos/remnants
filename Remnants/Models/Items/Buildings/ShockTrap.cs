using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Remnants
{
    class ShockTrap : Building
    {

        public struct Circle
        {
            public Vector2 Center { get; set; }
            public float Radius { get; set; }
        }

        public Texture2D halfCircle;
        public Texture2D lightningSegment;
        LightningBolt lb;
        Circle range;
		float dmgInterval = 1f;
		float lastDamageTime = 0f;

		public ShockTrap(ContentManager Content, Vector2 pos) : base(Content)
        {
            tilesWide = 1;
            tilesHigh = 1;
            position = pos;
            buildTime = 10f;
			resourceUsage[(int)resources.ENERGY] = 10;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
            LoadContent(Content);
            range = new Circle();
            range.Center = position + new Vector2(texture.Width / 2, texture.Height / 2);
            range.Radius = 350f;
            //lb = new LightningBolt(Position + new Vector2(texture.Width / 2, texture.Height / 2), Position + new Vector2(200, 200), Color.LightCyan);
			attackStrength = 10;
        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("buildings/shock_trap");
            halfCircle = Content.Load<Texture2D>("glowLine3");
            lightningSegment = Content.Load<Texture2D>("glowLine2");
            base.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

		public override void Update(GameTime gameTime, List<Unit> enemyUnits)
        {
			float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			lastDamageTime += deltaT;
			if (status == (int)buildingStates.OPERATIONAL && enemyUnits.Count > 0 && lastDamageTime >= dmgInterval)
            {
				// find nearest unit
				double minDist = double.MaxValue;
				int minI = 0;
				for (int i = 0; i < enemyUnits.Count; i++)
				{
					double dist;
					if ((dist = (enemyUnits[i].position - range.Center).Length()) < minDist)
					{
						minDist = dist;
						minI = i;
					}
				}

				if (WithinRange(enemyUnits[minI].position))
                {
                    if (lb == null)
                    {
						lb = new LightningBolt(position + new Vector2(texture.Width / 2, texture.Height / 2), enemyUnits[minI].position, Color.LightCyan);
                    }
					// deal damage
					enemyUnits[minI].dealDamage(this);
                }
                if (lb != null)
                {
                    lb.Update();
                    if (lb.Alpha <= 0)
                    {
                        lb = null;
                    }
                }
				lastDamageTime = 0f;
            }
			else
			{
				lb = null;
			}
            base.Update(gameTime);
        }
		public override void Update(GameTime gameTime, Vector2 p)
		{
			float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			lastDamageTime += deltaT;
			if (status == (int)buildingStates.OPERATIONAL)
			{
				if (WithinRange(p))
				{
					if (lb == null)
					{
						lb = new LightningBolt(position + new Vector2(texture.Width / 2, texture.Height / 2), p, Color.LightCyan);
					}
				}
				if (lb != null)
				{
					lb.Update();
					if (lb.Alpha <= 0)
					{
						lb = null;
					}
				}
			}
			else
			{
				lb = null;
			}
			base.Update(gameTime);
		}

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (lb != null)
            {
                lb.Draw(spriteBatch, halfCircle, lightningSegment);
            }
        }

        public override bool Place(Map map)
        {
            return base.Place(map);
        }

        bool WithinRange(Vector2 point)
        {
            return ((point - range.Center).Length() <= range.Radius);
        }
    }
}
