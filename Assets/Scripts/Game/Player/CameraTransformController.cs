using UnityEngine;

namespace Game.Player
{
    public class CameraTransformController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            InputToMove();
        }

        void InputToMove()
        {
            //TODO ここキーコンフィグに対応させる
            var move = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                move.y = 4;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move.y = -4;
            }
            if (Input.GetKey(KeyCode.A))
            {
                move.x = -4;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move.x = 4;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move *= 2;
            }
            transform.Translate(move * Time.deltaTime);
        }


        private static CameraTransformController _instace;
        public static CameraTransformController Instance
        {
            get
            {
                if (_instace == null) _instace = FindObjectOfType<CameraTransformController>();
                return _instace;
            }
        }
    }
}
