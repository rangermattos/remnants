using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class SmallHouse : Building
	{
		public SmallHouse(ContentManager Content, Vector2 pos) : base()
        {
			tilesWide = 1;
			tilesHigh = 1;
			Position = pos;
			buildTime = 10f;
            //woodCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.WOOD] = 100;
			//resourceCost[(int)resources.METAL] = 100;
			deltas[(int)resources.ENERGY] = -3;
            populationHousing = 2;

            LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/house_small");
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

	class MediumHouse : Building
	{
		public MediumHouse(ContentManager Content, Vector2 pos)
		{
			tilesWide = 2;
			tilesHigh = 1;
			Position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 200;
			resourceCost[(int)resources.METAL] = 100;
			resourceCost[(int)resources.ENERGY] = 200;
			deltas[(int)resources.ENERGY] = -10;
			populationHousing = 4;
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/house_medium");
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

	class LargeHouse : Building
	{
		public LargeHouse(ContentManager Content, Vector2 pos)
		{
			tilesWide = 2;
			tilesHigh = 1;
			Position = pos;
			buildTime = 10f;
            //metalCost = 200;
            //energyCost = 400;
			resourceCost[(int)resources.METAL] = 200;
			resourceCost[(int)resources.ENERGY] = 400;
			deltas[(int)resources.ENERGY] = -15;
			populationHousing = 8;
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/house_large");
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

