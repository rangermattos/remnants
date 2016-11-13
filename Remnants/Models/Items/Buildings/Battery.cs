using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class SmallBatteryFacility : Building
	{
		public SmallBatteryFacility(ContentManager Content, Vector2 pos) : base()
        {
			tilesWide = 1;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
            resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
            //energyStorage = 200;
			resourceStorage[(int)resources.ENERGY] = 200;
			animated = true;
			currentAnimation = new Animation(Content, "buildings/battery_small_spritesheet", 1f, 2, tilesHigh*64, tilesWide*64, true, true);
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/battery_small");
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

	class MediumBatteryFacility : Building
	{
		public MediumBatteryFacility(ContentManager Content, Vector2 pos)
		{
			tilesWide = 1;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 200;
            //energyCost = 200;
			resourceCost[(int)resources.ENERGY] = 200;
			resourceCost[(int)resources.METAL] = 200;
            //energyStorage = 400;
			resourceStorage[(int)resources.ENERGY] = 400;
			animated = true;
			currentAnimation = new Animation(Content, "buildings/battery_medium_spritesheet", 1f, 2, tilesHigh*64, tilesWide*64, true, true);
            LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/battery_medium");
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

	class LargeBatteryFacility : Building
	{
		public LargeBatteryFacility(ContentManager Content, Vector2 pos)
		{
			tilesWide = 1;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 400;
            //energyCost = 400;
			resourceCost[(int)resources.ENERGY] = 400;
			resourceCost[(int)resources.METAL] = 400;
            //energyStorage = 200;
			resourceStorage[(int)resources.ENERGY] = 800;
			animated = true;
			currentAnimation = new Animation(Content, "buildings/battery_large_spritesheet", 1f, 2, tilesHigh*64, tilesWide*64, true, true);
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/battery_large");
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

