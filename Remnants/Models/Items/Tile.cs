﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    public class Tile
    {
        public Texture2D texture;
        public Vector2 Position { get; set; }
        public bool canBuild = false;
        public bool canWalk = false;

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
