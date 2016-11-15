using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class AntimatterCreationLab : Building
	{
		public AntimatterCreationLab (ContentManager Content, Vector2 pos) : base(Content)
		{
			tilesWide = 1;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
			resourceCost[(int)resources.ENERGY] = 500;
			resourceCost[(int)resources.METAL] = 500;
			resourceGain[(int)resources.ANTIMATTER] = 1;
			resourceUsage[(int)resources.ENERGY] = 5;
			LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/antimatter_creation_lab_placeholder");
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

