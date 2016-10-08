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
        float xOffset;
        float yOffset;
        Tile[][] tiles;

        public Map(Vector2 mapSize)
        {
            xTiles = 70;
            yTiles = 40;
            xOffset = (mapSize.X - (xTiles * 64)) / 2;
            yOffset = (mapSize.Y - (yTiles * 64)) / 2;
            tiles = new Tile[xTiles][];
        }

        public void LoadContent(ContentManager Content)
        {
            tileList.Add(new MetalFloor(Content));

            GenerateMap(Content);
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Camera2D camera)
        {
            var cameraPos = camera.Position;
            var width = camera.Origin.X * 2;
            var height = camera.Origin.Y * 2;
            for (int i = 0; i < xTiles; i++)
            {
                for (int j = 0; j < yTiles; j++)
                {
                    //so we don't draw tiles that arent visible in camera
                    if (tiles[i][j].Position.X > (cameraPos.X - 64)
                        && tiles[i][j].Position.X < (cameraPos.X + width)
                        && tiles[i][j].Position.Y > (cameraPos.Y - 64)
                        && tiles[i][j].Position.Y < (cameraPos.Y + height))
                    {
                        tiles[i][j].Draw(spriteBatch);
                    }
                }
            }
        }

        void GenerateMap(ContentManager Content)
        {
            var r = new Random();

            for(int i = 0; i < xTiles; i++)
            {
                tiles[i] = new Tile[yTiles];
                for(int j = 0; j < yTiles; j++)
                {
                    int ind = r.Next(1);
                    tiles[i][j] = new MetalFloor(Content);
                    tiles[i][j].Position = new Vector2((64f * i) + xOffset, (64f * j) + yOffset);
                }
            }

        }
    }
}
