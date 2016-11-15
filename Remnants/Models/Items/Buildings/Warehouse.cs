using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class Warehouse : Building
	{
		public Warehouse(ContentManager Content, Vector2 pos) : base(Content)
        {
			tilesWide = 1;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
			resourceUsage[(int)resources.ENERGY] = 1;
			resourceStorage[(int)resources.WOOD] = 200;
			resourceStorage[(int)resources.METAL] = 200;
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/warehouse");
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

