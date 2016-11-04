using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Remnants
{
    public class Tile
    {
        public Vector2 Position { get; set; }
        public bool canBuild = false;
        public bool canWalk = false;
        public Texture2D texture;

        public virtual void LoadContent(ContentManager Content)
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position);
        }
    }
}
