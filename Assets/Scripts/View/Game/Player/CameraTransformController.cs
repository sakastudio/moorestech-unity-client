using UnityEngine;

namespace View.Game.Player
{
    public class CameraTransformController : MonoBehaviour
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
                move.y = CameraSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move.y = -CameraSpeed;
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
                transform.position = new Vector3(-CameraChunkSize, transform.position.y);
            }
            if (transform.position.x < -CameraChunkSize)
            {
                CameraChunk.x--;
                transform.position = new Vector3(CameraChunkSize, transform.position.y);
            }
            
            //Y loop
            if (CameraChunkSize < transform.position.y)
            {
                CameraChunk.y++;
                transform.position = new Vector3(transform.position.x, -CameraChunkSize);
            }
            if (transform.position.y < -CameraChunkSize)
            {
                CameraChunk.y--;
                transform.position = new Vector3(transform.position.x, CameraChunkSize);
            }
        }

        public Vector3 GetCameraPosition()
        {
            return transform.position + CameraChunk * CameraChunkSize * 2;
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
