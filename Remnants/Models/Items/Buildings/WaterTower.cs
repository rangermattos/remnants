using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class WaterTower : Building
	{
		public WaterTower (ContentManager Content, Vector2 pos) : base()
        {
			tilesWide = 1;
			tilesHigh = 2;
			Position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
            resourceCost[6] = 100;
            resourceCost[2] = 100;
            //waterStorage = 400;
            resourceStorage[1] = 400;
            deltas[2] = -1;
			LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/water_tower");
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

