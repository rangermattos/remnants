﻿using Microsoft.Xna.Framework;
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
        PathNode nextNode;
        Vector3 position;
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
    }
}