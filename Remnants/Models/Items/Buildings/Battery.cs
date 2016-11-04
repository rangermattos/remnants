using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class BatterySmall : Building
	{
		public BatterySmall(ContentManager Content, Vector2 pos) : base()
        {
			tilesWide = 1;
			tilesHigh = 1;
			Position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
            resourceCost[6] = 100;
            resourceCost[2] = 100;
            //energyStorage = 200;
            resourceStorage[3] = 200;
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

	class BatteryMedium : Building
	{
		public BatteryMedium(ContentManager Content, Vector2 pos)
		{
			tilesWide = 1;
			tilesHigh = 1;
			Position = pos;
			buildTime = 10f;
            //metalCost = 200;
            //energyCost = 200;
            resourceCost[6] = 200;
            resourceCost[2] = 200;
            //energyStorage = 400;
            resourceStorage[3] = 400;
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

	class BatteryLarge : Building
	{
		public BatteryLarge(ContentManager Content, Vector2 pos)
		{
			tilesWide = 1;
			tilesHigh = 1;
			Position = pos;
			buildTime = 10f;
            //metalCost = 400;
            //energyCost = 400;
            resourceCost[6] = 400;
            resourceCost[2] = 400;
            //energyStorage = 200;
            resourceStorage[3] = 800;
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

