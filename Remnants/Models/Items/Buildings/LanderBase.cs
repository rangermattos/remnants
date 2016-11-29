using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Remnants.Models;

namespace Remnants
{
	class LanderBase : Building
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

		public LanderBase(ContentManager Content, Vector2 pos) : base(Content)
		{
			tilesWide = 4;
			tilesHigh = 4;
			position = pos;
			buildTime = 0f;
			// costs nothing to build
			resourceStorage[(int)resources.FOOD] = 1000;
			resourceStorage[(int)resources.WATER] = 1000;
			resourceStorage[(int)resources.ENERGY] = 1000;
			resourceStorage[(int)resources.WOOD] = 1000;
			resourceStorage[(int)resources.METAL] = 1000;
			resourceStorage[(int)resources.POP] = 10;

			LoadContent(Content);

			range = new Circle();
			range.Center = position + new Vector2(texture.Width / 2, texture.Height / 2);
			range.Radius = 1000f;
			//lb = new LightningBolt(Position + new Vector2(texture.Width / 2, texture.Height / 2), Position + new Vector2(200, 200), Color.LightCyan);
			attackStrength = 10;

			animated = true;
			currentAnimation = new Animation(Content, "buildings/base_spritesheet", 1f, 2, tilesHigh*64, tilesWide*64, true, true);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/base");
			halfCircle = Content.Load<Texture2D>("glowLine3");
			lightningSegment = Content.Load<Texture2D>("glowLine2");
			base.LoadContent(Content);
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
		}


		public override void Update(GameTime gameTime, Level level)
        {
            Vector2 p = level.getCameraVector();
            float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
            lastDamageTime += deltaT;
            if (lb != null)
            {
                lb.Update();
                if (lb.Alpha <= 0)
                {
                    lb = null;
                }
            }
            if (status == (int)buildingStates.OPERATIONAL)
            {
                if (lastDamageTime >= dmgInterval)
                {
                    //find nearby enemies to attack
                    lastDamageTime = 0;
                    List<Entity> toAttack = level.getNearbyEnemies(this, 64 * 5);
                    foreach (Entity e in toAttack)
                    {
                        if (WithinRange(e.position))
                        {
                            if (lb == null)
                            {
                                e.dealDamage(this);
                                lb = new LightningBolt(position + new Vector2(texture.Width / 2, texture.Height / 2), e.position, Color.LightCyan);
                            }
                        }
                        //Console.Out.WriteLine("DAMAGING UNIT!");
                    }
                }
            }
            else
            {
                lb = null;
            }
            base.Update(gameTime, level);
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
