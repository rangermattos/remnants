using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
    class Water : Tile
    {
        public Water(ContentManager Content)
        {
            LoadContent(Content);
        }
        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("water");
            canBuild = false;
            canWalk = false;
            base.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
