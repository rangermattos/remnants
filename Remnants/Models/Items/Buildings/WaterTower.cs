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
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
            //waterStorage = 400;
			resourceStorage[(int)resources.WATER] = 400;
			resourceUsage[(int)resources.ENERGY] = 1;
			animated = true;
			currentAnimation = new Animation(Content, "buildings/water_tower_spritesheet", 1f, 7, tilesHigh*64, tilesWide*64, true, true);
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

