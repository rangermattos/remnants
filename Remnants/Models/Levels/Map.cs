using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Remnants
{
    class Map
    {
        public List<Tile> tileList = new List<Tile>();
        int xTiles;
        int yTiles;
        Tile[][] tiles;

        public Map()
        {
            xTiles = 70;
            yTiles = 40;
            tiles = new Tile[xTiles][];
        }

        public void LoadContent(ContentManager Content)
        {
            tileList.Add(new MetalFloor(Content));
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        void GenerateMap()
        {
            var r = new Random();



        }
    }
}
