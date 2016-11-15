using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	class Mine : Building
	{
		public Mine(ContentManager Content, Vector2 pos) : base(Content)
        {
			tilesWide = 2;
			tilesHigh = 1;
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
			resourceGain[(int)resources.METAL] = 10;
			resourceUsage[(int)resources.ENERGY] = 10;
			LoadContent(Content);
		}
		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/mine");
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
			// must be built on at least 1 ore tile
			// check that all tiles the building will be on can be built on
			bool hasOre = false;
			for(int i = 0; i < tilesWide; i++)
			{
				for(int j = 0; j < tilesHigh; j++)
				{
					if(map.GetTile(position + new Vector2(i * 64, j * 64)) == null)
					{
						return false;
					}
					if (map.GetTile(position + new Vector2(i * 64, j * 64)) is Ore)
					{
						hasOre = true;
					}
				}
			}
			if (!hasOre)
			{
				UI.Instance.EnqueueMessage("Mine must be built on at least 1 ore tile");
				return false;
			}
			return base.Place(map);
		}
	}
}

