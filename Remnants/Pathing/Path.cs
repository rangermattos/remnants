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
        public bool followPath(Unit e)
        {
            if (rootNode == null)
                return false;
            float dx = e.position.X - rootNode.position.X;
            float dy = e.position.Y - rootNode.position.Y;
            if(Math.Abs(dx) < 3 && Math.Abs(dy) < 3)
            {
                //Console.Out.WriteLine("POPPING NODE!");
                //we passed the node, so pop it off, if this happens to be the last node, return false
                rootNode = rootNode.nextNode;
                if (rootNode == null)
                    return false;
                dx = e.position.X - rootNode.position.X;
                dy = e.position.Y - rootNode.position.Y;
            }
            if(dx >= 0)
            {
                dx = Math.Min(e.moveSpeed, dx);
            }
            else
            {
                dx = Math.Max(-e.moveSpeed, dx);
            }
            if(dy >= 0)
            {
                dy = Math.Min(e.moveSpeed, dy);
            }
            else
            {
                dy = Math.Max(-e.moveSpeed, dy);
            }
            e.position = new Vector2(e.position.X - dx, e.position.Y - dy);
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
                PathNode n = new PathNode();
                n.nextNode = rootNode;
                n.position = pos;
                rootNode = n;
            }
        }
    }
}
