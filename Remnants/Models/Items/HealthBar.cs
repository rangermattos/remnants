using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	public class HealthBar
	{
		public Texture2D container, bar;
		public Vector2 barScale;
		public Vector2 topLeft { get; set; }
		public Vector2 position { get; set; }
		public float percent { get; set; }

		public HealthBar (ContentManager Content, Vector2 tl, Vector2 pos)
		{
			topLeft = tl;
			position = pos;

			barScale = new Vector2 (0f, 8.0f); // initial width = 0, height = 8
			LoadContent(Content);
		}

		public virtual void LoadContent(ContentManager Content)
		{
			container = Content.Load<Texture2D> ("health_bar_container");
			bar = Content.Load<Texture2D> ("health_bar_pixel");
		}

		public virtual void UnloadContent()
		{

		}

		public void Update()
		{
			barScale.X = percent * 32;
			//Console.Write("Pos: " + position + "\n");
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (percent != 1.0f)
			{
				spriteBatch.Draw(container, position);
				spriteBatch.Draw(bar, position, scale:barScale);
			}
		}
	}
}

