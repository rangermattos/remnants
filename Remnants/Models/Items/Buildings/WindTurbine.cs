using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
	class WindTurbine : Building
	{
		public WindTurbine (ContentManager Content, Vector2 pos) : base()
		{
			tilesWide = 2;
			tilesHigh = 2;
			//pos.Y -= (tilesHigh-1) * 64;
			position = pos;
			buildTime = 10f;
            //metalCost = 100;
            //energyCost = 100;
			resourceCost[(int)resources.ENERGY] = 100;
			resourceCost[(int)resources.METAL] = 100;
			resourceGain[(int)resources.ENERGY] = 15;
			animated = true;
			animation = new Animation(Content, "buildings/wind_turbine_spritesheet", 0.1f, 10, tilesHigh*64, tilesWide*64, true, true);
			LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("buildings/wind_turbine");
			//animation.LoadContent(Content, "buildings/wind_turbine_spritesheet");
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

