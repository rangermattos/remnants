using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using System.Collections.Generic;
using System;

namespace Remnants
{
    public class Map
    {
        List<Tile> tileList = new List<Tile>();
        int xTiles;
        int yTiles;
        /*/
    float xOffset;
    float yOffset;
            /*/
        public Tile[][] tiles;

        public Map()
        {
            xTiles = 70;
            yTiles = 40;
            LevelData.Instance.mapSize = new Vector2(xTiles, yTiles);
            //offsets are divided by 64, rounded down, and then scaled back up to keep them in line with 64x64 grid
            /*/
            xOffset = 64 * (float)Math.Floor(((mapSize.X - (xTiles * 64)) / 2)/64);
                yOffset = 64 * (float)Math.Floor(((mapSize.Y - (yTiles * 64)) / 2)/64);
                /*/
            tiles = new Tile[xTiles][];
        }
        
        public Map(Vector2 mapSize)
        {
            xTiles = (int)mapSize.X;
            yTiles = (int)mapSize.Y;
            tiles = new Tile[xTiles][];
            for (int i = 0; i < xTiles; i++)
            {
                tiles[i] = new Tile[yTiles];
            }
        }

        public void LoadContent(ContentManager Content)
        {
            tileList.Add(new MetalFloor(Content));
            tileList.Add(new Water(Content));
			tileList.Add(new Grass(Content));
			tileList.Add(new Ore(Content));
            if(LevelData.Instance.tileList.Count == 0)
                GenerateMap(Content);
            else
            {
                CreateMap(Content);
            }
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var cameraPos = Camera.Instance.cam.Position;
            var width = Camera.Instance.cam.Origin.X * 2;
            var height = Camera.Instance.cam.Origin.Y * 2;
            var z = Camera.Instance.cam.Zoom;
            var rect = Camera.Instance.cam.GetBoundingRectangle();
            for (int i = 0; i < xTiles; i++)
            {
                for (int j = 0; j < yTiles; j++)
                {
                    //tiles[i][j].Draw(spriteBatch);
                    //so we don't draw tiles that arent visible in camera
                    /*
                    if (tiles[i][j].Position.X > (cameraPos.X - 3 * (64 / z))
                        && tiles[i][j].Position.X < (cameraPos.X + width / z)
                        && tiles[i][j].Position.Y > (cameraPos.Y - 3 * (64 / z))
                        && tiles[i][j].Position.Y < (cameraPos.Y + height / z))
                        
                    */
                    if(tiles[i][j].Position.X > rect.Left - 2 * 64 * z
                        && tiles[i][j].Position.X < rect.Right
                        && tiles[i][j].Position.Y > rect.Top - 2 * 64 * z
                        && tiles[i][j].Position.Y < rect.Bottom)
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
					if (ind < 3)
						tiles[i][j] = new Ore(Content);
                    else if (ind < 90)
						tiles[i][j] = new Grass(Content);
                    else if (ind >= 90)
                        tiles[i][j] = new Water(Content);
                    tiles[i][j].Position = new Vector2((64f * i), (64f * j));
                    LevelData.Instance.tileList.Add(new LevelData.tileData(i, j, tiles[i][j]));
                }
            }
        }

        void CreateMap(ContentManager Content)
        {
            foreach (LevelData.tileData tile in LevelData.Instance.tileList)
            {
                if (tile.type == "Grass")
                    tiles[tile.xPosition][tile.yPosition] = new Grass(Content);
				else if (tile.type == "Ore")
					tiles[tile.xPosition][tile.yPosition] = new Ore(Content);
                else if(tile.type == "Water")
                    tiles[tile.xPosition][tile.yPosition] = new Water(Content);
                tiles[tile.xPosition][tile.yPosition].Position = new Vector2((64f * tile.xPosition), (64f * tile.yPosition));
            }
        }

        public Tile GetTile(Vector2 location)
        {
            try
            {
                return tiles[(int)(location.X / 64)][(int)(location.Y / 64)];
            }
            catch(Exception e) { return null; }
        }
    }
}
