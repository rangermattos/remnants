using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class NuclearStorage : Building
	{
		public NuclearStorage (ContentManager Content, Vector2 pos) : base(Content)
		{
			tilesWide = 2;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
			resourceStorage[(int)resources.FOOD] = 200;
			resourceUsage[(int)resources.ENERGY] = 2;

			LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/nuclear_storage_placeholder");
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

