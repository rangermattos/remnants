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
            //offsets are divided by 64, rounded down, and then scaled back up to keep them in line with 64x64 grid
            xOffset = 64 * (float)Math.Floor(((mapSize.X - (xTiles * 64)) / 2)/64);
            yOffset = 64 * (float)Math.Floor(((mapSize.Y - (yTiles * 64)) / 2)/64);
            tiles = new Tile[xTiles][];
        }

        public void LoadContent(ContentManager Content)
        {
            tileList.Add(new MetalFloor(Content));
            tileList.Add(new Water(Content));
			tileList.Add(new Grass(Content));
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
            var z = camera.Zoom;
            for (int i = 0; i < xTiles; i++)
            {
                for (int j = 0; j < yTiles; j++)
                {
                    //so we don't draw tiles that arent visible in camera
                    if (tiles[i][j].Position.X > (cameraPos.X - 3 * (64 / z))
                        && tiles[i][j].Position.X < (cameraPos.X + width / z)
                        && tiles[i][j].Position.Y > (cameraPos.Y - 3 * (64 / z))
                        && tiles[i][j].Position.Y < (cameraPos.Y + height / z))
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
                    int ind = r.Next(100);
                    if (ind < 80)
                        //tiles[i][j] = new MetalFloor(Content);
						tiles[i][j] = new Grass(Content);
                    else if (ind >= 80)
                        tiles[i][j] = new Water(Content);
                    tiles[i][j].Position = new Vector2((64f * i) + xOffset, (64f * j) + yOffset);
                }
            }
        }

        public Tile GetTile(Vector2 location)
        {
            Tile t;
            t = tiles[0][0];
            return t;
        }
    }
}
