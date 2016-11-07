using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class Greenhouse : Building
	{
		public Greenhouse (ContentManager Content, Vector2 pos) : base()
        {
			tilesWide = 1;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
			resourceGain[(int)resources.FOOD] = 10;
			resourceUsage[(int)resources.ENERGY] = 5;
			LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/greenhouse");
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

