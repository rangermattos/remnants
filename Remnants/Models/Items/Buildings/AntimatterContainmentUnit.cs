using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class AntimatterContainmentUnit : Building
	{
		public AntimatterContainmentUnit(ContentManager Content, Vector2 pos) : base(Content)
		{
			name = "Antimatter Containment Unit";
			tilesWide = 1;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
			resourceCost[(int)resources.ENERGY] = 500;
			resourceCost[(int)resources.METAL] = 500;
			resourceUsage[(int)resources.ENERGY] = 25;
			resourceStorage[(int)resources.ANTIMATTER] = 100;
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/antimatter_containment_unit");
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

