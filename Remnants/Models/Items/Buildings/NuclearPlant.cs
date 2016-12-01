using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class NuclearPlant : Building
	{
		public NuclearPlant(ContentManager Content, Vector2 pos) : base(Content)
		{
			tilesWide = 2;
			tilesHigh = 2;
			position = pos;
			buildTime = 10f;
			resourceCost[(int)resources.ENERGY] = 200;
			resourceCost[(int)resources.METAL] = 200;
			resourceGain[(int)resources.ENERGY] = 30;
			resourceUsage[(int)resources.NUCLEAR] = 5;
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/nuclear_plant_placeholder");
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

