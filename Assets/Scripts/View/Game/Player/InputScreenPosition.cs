using System;
using UnityEngine;
using Util;
using ViewModel.Block;

namespace View.Game.Player
{
    public class InputScreenPosition : SingletonMonoBehaviour<MonoBehaviour>
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var click = CameraTransformController.Instance.World2CameraPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                var clickBlockPos = new Vector2Int(Mathf.RoundToInt(click.x), Mathf.RoundToInt(click.y));
                PutBlockProcess.SendPutBlock(clickBlockPos,0);
            }
        }
    }
}