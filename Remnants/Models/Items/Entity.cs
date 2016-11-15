using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Remnants.Models
{
    public class Entity
    {
        //GlobalDamage as defined in the GDD
        public static float GLOBAL_DAMAGE = 10.0f;
        //used to show damage as a smooth missing chunk, rather than just dissapearing immediatly
        public float barhp;
        public float hp;
        //100 for units, 1000 for buildings, and 10000 for the main base
        public float hpMax;
        public float attackStrength;
        public float defenseStrength;
		protected HealthBar healthBar;
        // The ID used for registry, note that building and unit registries will overlap
        public int ID;
        // unique name for unit / building, however may be contained in both unit and building registry
		public String name;
		// the entity's position in the world
		public Vector2 position { get; set; }
		protected int width = 32, height = 32; // defaults
		// entity animations, may have more than one
		protected bool animated = false;
		protected Animation currentAnimation;
		// base texture and mask
		public Texture2D texture;
		public Color mask = Color.White;
		public float alpha;

		public Entity(ContentManager Content)
		{
			healthBar = new HealthBar(Content, position, position);
		}

        //used to set up the unit / buildings stats
        public virtual void Init()
        {
            //defaults to a unit
            hp = 100;
            hpMax = 100;
            attackStrength = 10;
            defenseStrength = 10;
        }
        public void dealDamage(Entity attacker)
        {
            float damage = (attacker.attackStrength / defenseStrength) * GLOBAL_DAMAGE;
            hp -= damage;
        }

		public virtual void Update(GameTime gameTime, Level level)
		{
			healthBar.percent = hp/hpMax;
			healthBar.Update();
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (animated)
			{
				currentAnimation.Draw(spriteBatch, position, mask * alpha);
			}
			else
			{
				spriteBatch.Draw(texture, position, mask * alpha);
			}
			healthBar.Draw(spriteBatch);
		}
    }
}
