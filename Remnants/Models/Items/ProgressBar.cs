using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	public class ProgressBar
	{
		public Texture2D container, bar;
		public Vector2 barScale;
		public Vector2 topLeft { get; set; }
		public Vector2 position { get; set; }
		public float progress { get; set; }

		public virtual void LoadContent(ContentManager Content)
		{

		}

		public virtual void UnloadContent()
		{

		}

		public ProgressBar (ContentManager Content, Vector2 tl, Vector2 pos)
		{
			topLeft = tl;
			position = pos;

			container = Content.Load<Texture2D> ("progress_bar_container");
			bar = Content.Load<Texture2D> ("progress_bar_pixel");
			barScale = new Vector2 (0f, 8.0f); // initial width = 0, height = 8
		}

		/*public void Draw(SpriteBatch spriteBatch)
		{
			if (progress != 1.0f)
			{
				spriteBatch.Draw(container, position);
				spriteBatch.Draw(bar, position, scale:barScale);
			}
		}*/
	}
}

