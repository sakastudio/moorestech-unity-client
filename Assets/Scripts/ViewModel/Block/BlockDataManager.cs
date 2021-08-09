using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUI.ViewModel.Block
{
    public class BlockDataManager
    {
        private static readonly Dictionary<Coordinate,Block> CoordinateDictionary = new Dictionary<Coordinate, Block>();
        public int[,] GetBlocks(Vector2Int start,Vector2Int end)
        {
            var xSize = Math.Abs(start.x - end.x);
            var ySize = Math.Abs(start.y - end.y);
            var blocks = new int[xSize,ySize];
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    var c = new Coordinate {x = i + start.x, y = i + start.y};
                    if (CoordinateDictionary.ContainsKey(c))
                    {
                        blocks[i, j] = CoordinateDictionary[c].id;
                    }
                    else
                    {
                        blocks[i, j] = -1;
                    }
                }
            }
            return blocks;
        }
        
        
        
        private struct Coordinate
        {
            public int x;
            public int y;
        }
        private struct Block
        {
            public int id;
            public int intId;
        }
    }
}
