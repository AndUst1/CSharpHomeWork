using UnityEngine;

namespace Maze
{
    public class Player : MonoBehaviour
    {
        [SerializeField] protected float _speed = 4;
        [SerializeField] protected short _jumpForce = 150;
        [SerializeField] protected float _rotationSpeed = 10;
        private Rigidbody _rb;
        private Vector3 _direction;
        public bool _isGrounded;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected void Move()
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
                _speed = 5;
            else
                _speed = 4;


            Vector3 directionVector = new Vector3(_direction.x, 0, _direction.z);

            if (directionVector.magnitude > Mathf.Abs(0.07f))
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * _rotationSpeed);

        }

        private void FixedUpdate()
        {
            transform.position = transform.position + _direction * Time.fixedDeltaTime * _speed;
        }

        protected void OnCollisionEnter()
        {
            _isGrounded = true;
        }

        protected void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _isGrounded = false;
                GetComponent<Rigidbody>().AddForce(new Vector3(0, _jumpForce, 0));
            }
        }
    }
}
