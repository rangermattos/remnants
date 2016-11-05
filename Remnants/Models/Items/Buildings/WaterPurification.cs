﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class WaterPurification : Building
	{
		public WaterPurification(ContentManager Content, Vector2 pos) : base()
        {
			tilesWide = 2;
			tilesHigh = 2;
			Position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
            resourceCost[6] = 100;
            resourceCost[2] = 100;
            deltas[1] = 10;
            deltas[2] = -5;
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/water_purification");
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
