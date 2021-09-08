using System;
using System.Collections.Generic;
using UnityEngine;
using Util;
using View.Game.Player;
using ViewModel.Block;

namespace View.Game.Tilemap
{
    public class DisplayBlock : SingletonMonoBehaviour<DisplayBlock>
    {
        private readonly Vector2Int _displayBlockRange = new Vector2Int(15,15);
        private GameObject[,] blockObjects;
        [SerializeField] private GameObject blockTilePrefab;

        private void Start()
        {
            blockObjects = new GameObject[_displayBlockRange.x * 2 + 1, _displayBlockRange.y * 2 + 1];
            for (int i = 0; i < blockObjects.GetLength(0); i++)
            {
                for (int j = 0; j < blockObjects.GetLength(1); j++)
                {
                    blockObjects[i, j] = Instantiate(blockTilePrefab,transform);
                    blockObjects[i, j].SetActive(false);
                }   
            }
        }

        private void FixedUpdate()
        {
            var p = CameraTransformController.Instance.GetCameraPosition();
            Vector2Int cameraPos = new Vector2Int(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y));
            var blocks = BlockDataManager.Instance.GetBlocks(cameraPos - _displayBlockRange, cameraPos + _displayBlockRange);

            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    if (blocks[i,j] == -1)
                    {
                        blockObjects[i, j].SetActive(false);
                        continue;
                    }
                    blockObjects[i, j].SetActive(true);
                    blockObjects[i, j].transform.position = new Vector2(i, j) - cameraPos;
                }
            }
        }
    }
}