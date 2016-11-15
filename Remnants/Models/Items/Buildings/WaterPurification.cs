using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class WaterPurification : Building
	{
		public WaterPurification(ContentManager Content, Vector2 pos) : base(Content)
        {
			tilesWide = 2;
			tilesHigh = 2;
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
			resourceGain[(int)resources.WATER] = 10;
			resourceUsage[(int)resources.ENERGY] = 5;
			animated = true;
			currentAnimation = new Animation(Content, "buildings/water_purification_spritesheet", 0.1f, 5, tilesHigh*64, tilesWide*64, true, true);
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

