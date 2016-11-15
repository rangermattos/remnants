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

		public Unit(ContentManager Content) : base(Content)
        {
            alpha = 1.0f;
			LoadContent(Content);
			Init();
        }
		public virtual void LoadContent(ContentManager content)
        {
            //texture = content.Load<Texture2D>("buildings/battery_small");
            var p = new Vector2(position.X + 32, position.Y);
            
        }
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
            position = new Vector2(position.X + moveSpeed, position.Y);
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
    }
}
