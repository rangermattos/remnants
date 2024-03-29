﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class SmallHouse : Building
	{
		public SmallHouse(ContentManager Content, Vector2 pos) : base(Content)
        {
			name = "Small House";
            workersNeeded = 0;
            tilesWide = 1;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //woodCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			//resourceCost[(int)resources.WOOD] = 100;
			resourceCost[(int)resources.METAL] = 50;
			resourceUsage[(int)resources.ENERGY] = 5;
			resourceStorage[(int)resources.POP] = 2;
			canDisable = false;

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

        public override void Update(GameTime gameTime, Level level)
        {
            base.Update(gameTime, level);
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
		public MediumHouse(ContentManager Content, Vector2 pos) : base(Content)
		{
			name = "Medium House";
            workersNeeded = 0;
            tilesWide = 2;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 200;
			resourceCost[(int)resources.METAL] = 100;
			resourceCost[(int)resources.ENERGY] = 200;
			resourceUsage[(int)resources.ENERGY] = 10;
			resourceStorage[(int)resources.POP] = 4;
			canDisable = false;

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

        public override void Update(GameTime gameTime, Level level)
        {
            base.Update(gameTime, level);
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
		public LargeHouse(ContentManager Content, Vector2 pos) : base(Content)
		{
			name = "Large House";
            workersNeeded = 0;
            tilesWide = 2;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 200;
            //energyCost = 400;
			resourceCost[(int)resources.METAL] = 200;
			resourceCost[(int)resources.ENERGY] = 400;
			resourceUsage[(int)resources.ENERGY] = 15;
			resourceStorage[(int)resources.POP] = 8;
			canDisable = false;

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

        public override void Update(GameTime gameTime, Level level)
        {
            base.Update(gameTime, level);
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

