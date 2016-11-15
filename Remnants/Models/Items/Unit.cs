using Remnants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    public class Unit : Entity
    {
        //team 0 = AGGRESSIVE TO ALL
        //team 1 = player owned
        //team 2+ = NPC owned
        public int team = 0;
        public float moveSpeed = 1.0f;
        protected Path followedPath = null;
        protected Texture2D pathNode, pathGoal;
		public Unit(ContentManager Content) : base(Content)
        {
            alpha = 1.0f;
			LoadContent(Content);
			Init();
        }
		public virtual void LoadContent(ContentManager content)
        {
            pathNode = content.Load<Texture2D>("icons/debug_path_node");
            pathGoal = content.Load<Texture2D>("icons/debug_path_goal");
            var p = new Vector2(position.X + 32, position.Y);
            
        }
        //TURN THIS FALSE TO DISABLE SHOWING THE PATH A UNIT IS FINDING!
        bool showPath = true;
		public override void Draw(SpriteBatch spriteBatch)
        {
            /*if (animated)
            {
                currentAnimation.Draw(spriteBatch, position, mask * alpha);
            }
            else
            {
                spriteBatch.Draw(texture, new Vector2(position.X + texture.Bounds.Width, position.Y), mask * alpha);
            }*/
            
            if(showPath && followedPath != null)
            {
                followedPath.draw(spriteBatch, pathNode, pathGoal);
            }
			base.Draw(spriteBatch);
            
        }
        public override void Init()
        {
            this.hp = 100;
            this.hpMax = 100;
            this.attackStrength = 10;
			this.defenseStrength = 10;
			base.Init();
        }
        public override void Update(GameTime gameTime, Level level)
        {
            Vector2 lastPos = position;
            unitUpdate(gameTime, level);
            if (!level.isPositionValid(position))
            {
                position = lastPos;
			}

			// update animation
			if (animated)
			{
				currentAnimation.Update(gameTime);
			}
			// update healthbar position
			healthBar.position = new Vector2(position.X + width/2 - healthBar.container.Width/2, position.Y);

			base.Update(gameTime, level);
        }
        //do your AI logics here, so that collision occurs at the valid time
        public virtual void unitUpdate(GameTime gameTime, Level l)
        {
            if(followedPath == null)
            {
                Vector2 target = new Vector2(position.X + (5 * 64), position.Y);
                followedPath = l.getPathToLocation(position, target);
            }
            else
            {
                if(!followedPath.followPath(this))
                {
                    followedPath = null;
                }
            }
        }
    }
}
