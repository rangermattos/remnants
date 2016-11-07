using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class Granary : Building
	{
		public Granary (ContentManager Content, Vector2 pos) : base()
        {
			tilesWide = 2;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
            //foodStorage = 400;
			resourceStorage[(int)resources.FOOD] = 400;
			resourceUsage[(int)resources.ENERGY] = 2;

            LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/granary");
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

