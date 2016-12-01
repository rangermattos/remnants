using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Remnants
{
	public class Animation
	{
		protected float frameTime;
		protected float currFrameTime = 0f;
		protected int frameIndex = 0;
		protected int totalFrames;
		protected int frameWidth, frameHeight;
		Rectangle frame;
		public Texture2D texture;
		public bool looping;
		public bool active;
		public bool Hflip = false;

		public Animation (ContentManager Content, string spriteSheet, float frameTime, int totalFrames, int frameHeight, int frameWidth, bool looping, bool active)
		{
			this.frameTime = frameTime;
			this.totalFrames = totalFrames;
			this.frameHeight = frameHeight;
			this.frameWidth = frameWidth;
			frame = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);
			this.looping = looping;
			this.active = active;
			LoadContent(Content, spriteSheet);
		}

		public void LoadContent(ContentManager Content, string spriteSheet)
		{
			texture = Content.Load<Texture2D>(spriteSheet);
		}

		public void UnloadContent()
		{

		}

		public void Update(GameTime gameTime)
		{
			if (!active)
				return;

			var deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			// animation frame changes
			currFrameTime += deltaT;
			if (currFrameTime > frameTime)
			{
				frameIndex++;
				//currFrameTime -= frameTime;
				currFrameTime = 0f;
			}
			if (frameIndex > totalFrames-1)
			{
				frameIndex = 0;
				if (!looping)
					active = false;
			}
			frame = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
		{
			if (active)
			{
				if (Hflip)
					spriteBatch.Draw(texture, position:position, sourceRectangle:frame, color:color, effects:SpriteEffects.FlipHorizontally);
				else
					spriteBatch.Draw(texture, position, frame, color);
			}
		}

		public void printDebugInfo()
		{
			Console.Write("Frame index: " + frameIndex + " Frame time: " + currFrameTime + "\n");
		}
	}
}

