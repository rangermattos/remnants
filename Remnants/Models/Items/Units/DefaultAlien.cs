using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
	public class DefaultAlien : Unit
	{
		Animation walkLeftAnimation;
		Animation walkRightAnimation;

		public DefaultAlien (ContentManager Content) : base(Content)
		{
			width = 16;
			height = 16;
			animated = true;
			walkLeftAnimation = new Animation(Content, "units/unnamed_alien/unnamed_alien_walk_left_spritesheet", .25f, 4, width, height, true, false);
			walkRightAnimation = new Animation(Content, "units/unnamed_alien/unnamed_alien_walk_right_spritesheet", .25f, 4, width, height, true, true);
			currentAnimation = walkLeftAnimation;
			LoadContent(Content);
		}

		public override void LoadContent(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("units/unnamed_alien/unnamed_alien_sharpened");
			base.LoadContent(Content);
		}

		public override void Update(GameTime gameTime, Level l)
		{
			if (position.X > lastPos.X)
			{
				// walking right
				currentAnimation = walkRightAnimation;
				Console.Write("Right: ");
				currentAnimation.printDebugInfo();
			}
			else if (position.X < lastPos.X)
			{
				// walking left
				currentAnimation = walkLeftAnimation;
				Console.Write("Left: ");
				currentAnimation.printDebugInfo();
			}
			base.Update(gameTime, l);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}

		/*public override void unitUpdate(GameTime gameTime, Level l)
		{
			if(followedPath == null)
			{
				// target nearest building
				//Vector2 target = new Vector2(position.X - (64 * 5), position.Y + 64);
				double minDist = double.MaxValue;
				int minI = 0;
				for (int i = 0; i < LevelData.Instance.buildingList.Count; i++)
				{
					double dist;
					if ((dist = (LevelData.Instance.buildingList[i].position - position).Length()) < minDist)
					{
						minDist = dist;
						minI = i;
					}
				}
				Vector2 target = LevelData.Instance.buildingList[minI].position;
				if(l.isPositionValid(target))
				{
					followedPath = l.getPathToLocation(position, target);
				}
				else
				{
					Console.Write("Position not valid! : " + target + "\n");
				}

				// update walking animation
				if (target.X < position.X)
				{
					currentAnimation = walkLeftAnimation;
					walkLeftAnimation.active = true;
				}
				else if (target.X > position.X)
				{
					Console.Write("Switching to walk right\n");
					currentAnimation = walkRightAnimation;
					walkRightAnimation.active = true;
				}
			}
			else
			{
				if(!followedPath.followPath(this))
				{
					followedPath = null;
				}
			}
		}*/
	}
}

