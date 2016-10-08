using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    class Level
    {
        Texture2D backGround;
        protected Map map;
        public Vector2 mapSize;

        public Level(SpriteFont font)
        {
            mapSize = new Vector2(5760, 3240);
            map = new Map(mapSize);
        }

        public void LoadContent(Game1 game)
        {
            backGround = game.Content.Load<Texture2D>("StarsBasic");
            map.LoadContent(game.Content);
        }

        public void UnloadContent(Game1 game)
        {
            game.Content.Unload();
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            //cameraController must be updated before everything else so that the Matrices are up to date
            //cameraController.Update(gameTime);
            //these bassicEffect updates must take place in order to draw properly
            //map.Update(worldMatrix, cameraController.GetProjection(), cameraController.GetView());
            

            //ui currently only has a readout for the camera position and target. these updates are called here
            //ui.Update(cameraController.GetCamPosition(), cameraController.GetCamTarget());
        }

        public void Draw(SpriteBatch spriteBatch, Camera2D camera)
        {
            var viewMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);
            //spriteBatch.Draw(backGround, new Rectangle(0, 0, 5760, 3240), Color.White);
            map.Draw(spriteBatch, camera);
            spriteBatch.End();

            //map.Draw(graphics);

            //ui.Draw(spriteBatch);
        }
    }
}
