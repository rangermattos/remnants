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
        float moveSpeed = 1.0f;
        Path followedPath = null;
        Texture2D pathNode, pathGoal;
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
            
            
			base.Draw(spriteBatch);
            if(showPath && followedPath != null)
            {
                followedPath.draw(spriteBatch, pathNode, pathGoal);
            }
        }
        public override void Init()
        {
            this.hp = 100;
            this.hpMax = 100;
            this.attackStrength = 10;
			this.defenseStrength = 10;
			base.Init();
        }
        public virtual void Update(GameTime gameTime, Level l)
        {
            Vector2 lastPos = position;
            unitUpdate(gameTime, l);
            if (!l.isPositionValid(position))
            {
                position = lastPos;
			}

			if (animated)
			{
				currentAnimation.Update(gameTime);
			}
			healthBar.position = new Vector2(position.X + width/2 - healthBar.container.Width/2, position.Y);
			base.Update(gameTime);
        }
        //do your AI logics here, so that collision occurs at the valid time
        public virtual void unitUpdate(GameTime gameTime, Level l)
        {
            if(followedPath == null)
            {
                Vector2 target = new Vector2(position.X - (64 * 5), position.Y);
                if(l.isPositionValid(target))
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
