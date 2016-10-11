using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
	class WindTurbine : Building
	{
		public WindTurbine (ContentManager Content, Vector2 pos)
		{
			tilesWide = 2;
			tilesHigh = 2;
			pos.Y -= (tilesHigh-1) * 64;
			Position = pos;
			buildTime = 10f;
			deltaEnergy = 10;
			metalCost = 100;
			energyCost = 100;
			LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("wind_turbine");
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

