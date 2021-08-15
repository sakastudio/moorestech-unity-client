using System;
using System.Collections.Generic;
using UnityEngine;
using Util.Block;

namespace ViewModel.Block
{
    public static class CollectBlock
    {
        public static int[,] Collect(Vector2Int start,Vector2Int end, Dictionary<Coordinate,BlockData> blockData)
        {
            var xSize = Math.Abs(start.x - end.x);
            var ySize = Math.Abs(start.y - end.y);
            var blocks = new int[xSize,ySize];
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    var c = new Coordinate {x = i + start.x, y = i + start.y};
                    if (blockData.ContainsKey(c))
                    {
                        blocks[i, j] = blockData[c].id;
                    }
                    else
                    {
                        blocks[i, j] = -1;
                    }
                }
            }
            return blocks;
        }
    }
}