using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    class MetalFloor : Tile
    {
        public MetalFloor(ContentManager Content)
        {
            LoadContent(Content);
        }
        protected override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("mtlFloor");
            base.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
