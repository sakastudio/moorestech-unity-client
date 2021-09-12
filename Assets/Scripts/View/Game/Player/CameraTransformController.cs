using UnityEngine;
using Util;

namespace View.Game.Player
{
    public class CameraTransformController : SingletonMonoBehaviour<CameraTransformController>
    {
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private int CameraSpeed = 4;
        [SerializeField] private int SprintMagnification = 2;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            InputToMove();
            LoopCamera();
        }

        void InputToMove()
        {
            //TODO ここキーコンフィグに対応させる
            var move = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                move.z = CameraSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move.z = -CameraSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                move.x = -CameraSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move.x = CameraSpeed;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move *= SprintMagnification;
            }
            transform.Translate(move * Time.deltaTime);
        }

        private const int CameraChunkSize = 50;
        [SerializeField]private Vector3 CameraChunk = Vector3.zero;
        void LoopCamera()
        {
            //X loop
            if (CameraChunkSize < transform.position.x)
            {
                CameraChunk.x++;
                transform.position = new Vector3(-CameraChunkSize,0, transform.position.z);
            }
            if (transform.position.x < -CameraChunkSize)
            {
                CameraChunk.x--;
                transform.position = new Vector3(CameraChunkSize,0, transform.position.z);
            }
            
            //Y loop
            if (CameraChunkSize < transform.position.z)
            {
                CameraChunk.z++;
                transform.position = new Vector3(transform.position.x,0, -CameraChunkSize);
            }
            if (transform.position.z < -CameraChunkSize)
            {
                CameraChunk.z--;
                transform.position = new Vector3(transform.position.x,0, CameraChunkSize);
            }
        }

        public Vector3 GetCameraPosition()
        {
            return transform.position + CameraChunk * CameraChunkSize * 2;
        }
        public Vector3 World2CameraPosition(Vector3 position)
        {
            return position + CameraChunk * CameraChunkSize * 2;
        }

        public Vector2 GetCameraChunk()
        {
            return CameraChunk;
        }


        private static CameraTransformController _instances;
        public static CameraTransformController Instance
        {
            get
            {
                if (_instances == null) _instances = FindObjectOfType<CameraTransformController>();
                return _instances;
            }
        }
    }
}
