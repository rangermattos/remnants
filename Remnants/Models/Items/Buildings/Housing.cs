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
            resourceCost[6] = 100;
            resourceCost[2] = 100;
            resourceCost[5] = 100;
            deltas[2] = -3;
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
            resourceCost[6] = 100;
            resourceCost[2] = 200;
            deltas[2] = -10;
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
            resourceCost[6] = 200;
            resourceCost[2] = 400;
            deltas[2] = -15;
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

