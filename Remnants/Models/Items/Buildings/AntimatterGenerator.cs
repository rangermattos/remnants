using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class AntimatterGenerator : Building
	{
		public AntimatterGenerator(ContentManager Content, Vector2 pos) : base(Content)
		{
			name = "Antimatter Generator";
			tilesWide = 2;
			tilesHigh = 2;
			position = pos;
			buildTime = 10f;
			resourceCost[(int)resources.ENERGY] = 500;
			resourceCost[(int)resources.METAL] = 500;
			resourceGain[(int)resources.ENERGY] = 50;
			resourceUsage[(int)resources.ANTIMATTER] = 5;
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/antimatter_generator");
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

