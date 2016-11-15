using Microsoft.Xna.Framework;
using Remnants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Remnants
{
    class PathNode
    {
        public PathNode nextNode = null;
        public Vector2 position;
    }
    public class Path
    {
        PathNode rootNode;
        float ldx = 0, ldy = 0;
        public bool followPath(Entity e)
        {
            if (rootNode == null)
                return false;
            float dx = e.position.X - rootNode.position.X;
            float dy = e.position.Y - rootNode.position.Y;
            int tridx = 0;
            int tridy = 0;
            int trildx = 0;
            int trildy = 0;
            if (dx > 0)
                tridx = 1;
            else if (dx < 0)
                tridx = -1;

            if (dy > 0)
                tridy = 1;
            else if (dy < 0)
                tridy = -1;

            if (ldx > 0)
                trildx = 1;
            else if (ldx < 0)
                trildx = -1;

            if (ldy > 0)
                trildy = 1;
            else if (ldy < 0)
                trildy = -1;

            if (ldx == 0 && ldy == 0)
            {
                ldx = dx;
                ldy = dy;
            }
            else if(Math.Abs(dx) < 3 && Math.Abs(dy) < 3)
            {
                //we passed the node, so pop it off, if this happens to be the last node, return false
                rootNode = rootNode.nextNode;
            }
            e.position = new Vector2(e.position.X - Math.Min(tridx, dx), e.position.Y - Math.Min(tridy, dy));
            ldx = dx;
            ldy = dy;
            return true;
        }

        public void draw(SpriteBatch spriteBatch, Texture2D pathNode, Texture2D pathGoal)
        {
            PathNode cur = rootNode;
            if(pathNode == null)
            {
                Console.Out.WriteLine("FUUU");
            }
            while(cur != null)
            {
                if(cur.position != null)
                    spriteBatch.Draw(pathNode, new Vector2(cur.position.X, cur.position.Y), Color.White);
                cur = cur.nextNode;
            }
        }

        public void addNode(Vector2 pos)
        {
            if(rootNode == null)
            {
                rootNode = new PathNode();
                rootNode.position = pos;
            }
            else
            {
                PathNode cur = rootNode;
                while(cur.nextNode != null)
                {
                    cur = cur.nextNode;
                }
                PathNode toAdd = new PathNode();
                toAdd.position = pos;
                cur.nextNode = toAdd;
            }
        }
    }
}
