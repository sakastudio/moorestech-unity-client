using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Util.Block;
using ViewModel.Block;

namespace Tests
{
    public class BlockDataManagerTest
    {
        [Test]
        public void GenerateBlockRequestPacketDataTest()
        {
            var start = new Vector2Int(0, 0);
            var end = new Vector2Int(10, 10);
            var ans = GenerateBlockRequestPacketData.Generate(start, end);
            Debug.Log(ans.Count);
            Assert.AreEqual(ans.Count,9);
            
            
            start = new Vector2Int(0, 0);
            end = new Vector2Int(0, 10);
            ans = GenerateBlockRequestPacketData.Generate(start, end); 
            Debug.Log(ans.Count);
            Assert.AreEqual(ans.Count,3);
            
            
            start = new Vector2Int(-10, -10);
            end = new Vector2Int(0, 0);
            ans = GenerateBlockRequestPacketData.Generate(start, end); 
            Debug.Log(ans.Count);
            Assert.AreEqual(ans.Count,9);
            
            
            start = new Vector2Int(-10, -10);
            end = new Vector2Int(10, 10);
            ans = GenerateBlockRequestPacketData.Generate(start, end); 
            Debug.Log(ans.Count);
            Assert.AreEqual(ans.Count,25);
        }

        [Test]
        public void CollectBlockTest()
        {
            var start = new Vector2Int(0, 0);
            var end = new Vector2Int(10, 10);
            var ans = CollectBlock.Collect(start,end,new Dictionary<Coordinate, BlockData>());
            Assert.AreEqual(ans.GetLength(0),11);
            Assert.AreEqual(ans.GetLength(1),11);
            
            start = new Vector2Int(-10, -10);
            end = new Vector2Int(0, 0);
            ans = CollectBlock.Collect(start,end,new Dictionary<Coordinate, BlockData>());
            Assert.AreEqual(ans.GetLength(0),11);
            Assert.AreEqual(ans.GetLength(1),11);
            
            start = new Vector2Int(-10, -10);
            end = new Vector2Int(10, 10);
            ans = CollectBlock.Collect(start,end,new Dictionary<Coordinate, BlockData>());
            Assert.AreEqual(ans.GetLength(0),21);
            Assert.AreEqual(ans.GetLength(1),21);
        }
    }
}