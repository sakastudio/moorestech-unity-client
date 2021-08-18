using System;
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
            Console.Write(ans.Count);
            Assert.Equals(ans.Count,9);
        }
    }
}