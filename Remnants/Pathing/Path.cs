using Microsoft.Xna.Framework;
using Remnants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool followPath(Entity e)
        {
            if (rootNode == null)
                return false;
            
            return false;
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
