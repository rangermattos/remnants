using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	public class DefaultAlien : Unit
	{
		public DefaultAlien (ContentManager Content) : base(Content)
		{
			width = 16;
			height = 16;
			animated = true;
			Animation walkLeftAnimation = new Animation(Content, "units/unnamed_alien/unnamed_alien_walk_left_spritesheet", .25f, 4, width, height, true, false);
			Animation walkRightAnimation= new Animation(Content, "units/unnamed_alien/unnamed_alien_walk_right_spritesheet", .25f, 4, width, height, true, true);
			currentAnimation = walkRightAnimation;
			LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("units/unnamed_alien/unnamed_alien_sharpened");
			base.LoadContent(Content);
		}

		public override void Update(GameTime gameTime, Level l)
		{
			base.Update(gameTime, l);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}

