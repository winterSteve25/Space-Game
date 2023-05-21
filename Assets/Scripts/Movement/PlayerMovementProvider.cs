using UnityEngine;

namespace Movement
{
    public class PlayerMovementProvider : MonoBehaviour, IMovementProvider
    {
        public Vector3 Direction { get; private set; }
        public float Speed => speed;
        public Vector3 Rotation => _rotation;
        public bool Jump { get; private set; }
        public float JumpForce => jumpForce;
        public float AirSpeedMultiplier => airSpeedMultiplier;

        [SerializeField] private float speed;
        [SerializeField] private float sensitivityX;
        [SerializeField] private float sensitivityY;
        
        [SerializeField] private float jumpForce;
        [SerializeField] private float airSpeedMultiplier;
        
        private Transform _transform;
        private Vector3 _rotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _transform = transform;
            _rotation = Vector3.zero;
        }

        private void Update()
        {
            var mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensitivityX;
            var mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensitivityY;

            // x and y swapped because horizontal movement rotates the character on the y axis
            // and vice versa
            _rotation.y += mouseX;
            _rotation.x -= mouseY;
            
            // prevent going too high or low
            _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

            Jump = Input.GetKey(KeyCode.Space);
        }

        private void FixedUpdate()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveZ = Input.GetAxisRaw("Vertical");
            Direction = (_transform.right * moveX + _transform.forward * moveZ).normalized;
        }
    }
}