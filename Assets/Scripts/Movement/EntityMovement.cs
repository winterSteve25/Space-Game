using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Movement
{
    /// <summary>
    /// Base EntityMovement script does not move on its own
    /// Attach a script that implements <see cref="IMovementProvider"/> on the same game object
    /// This script will take input from the movement provider and move
    /// </summary>
    [RequireComponent(typeof(IMovementProvider), typeof(Rigidbody))]
    public class EntityMovement : MonoBehaviour
    {
        private const float SpeedMultiplierConstant = 10f;
        
        private IMovementProvider _movementProvider;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private float _speedMultiplier;
        
        private bool _grounded;
        private bool _canJump;
        
        [BoxGroup("Ground Checking"), SerializeField] private float groundDrag = 5;
        [BoxGroup("Ground Checking"), SerializeField] private LayerMask groundLayer;
        
        [BoxGroup("Camera"), SerializeField] private Optional<Camera> cam;
        [BoxGroup("Camera"), SerializeField, Required, ShowIf("@cam.Exists()")] private Transform camPosition;
        
        private void Start()
        {
            _movementProvider = GetComponent<IMovementProvider>();
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _speedMultiplier = SpeedMultiplierConstant;
            _canJump = true;
        }

        private void Update()
        {
            // rotate character
            _transform.rotation = Quaternion.Euler(0, _movementProvider.Rotation.y, 0);
            GroundCheck();
            UpdateCamera();
        }

        private void FixedUpdate()
        {
            CheckJump();
            ApplyMove();
            LimitVelocity();
        }

        private void ResetJump()
        {
            _canJump = true;
        }

        private void CheckJump()
        {
            // check jump 
            if (!_movementProvider.Jump || !_grounded || !_canJump) return;
            _canJump = false;
            // reset current y velocity
            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, 0, velocity.z);
            _rigidbody.velocity = velocity;
            _rigidbody.AddForce(transform.up * _movementProvider.JumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), 0.15f);
        }

        private void ApplyMove()
        {
            if (_movementProvider.Direction == Vector3.zero) return;
            var airSpeedMultiplier = _grounded ? 1 : _movementProvider.AirSpeedMultiplier;
            _rigidbody.AddForce(_movementProvider.Direction * (_movementProvider.Speed * _speedMultiplier * airSpeedMultiplier), ForceMode.Force);
        }

        private void UpdateCamera()
        {
            // not using cam.RunIfExists because using callbacks creates unnecessary garbage
            // and this is called frequently
            if (!cam.Exists()) return;

            // caching to reduce extern calls
            var cam1 = cam.Unwrap();
            var transform1 = cam1.transform;

            // rotate camera and update its position
            transform1.rotation = Quaternion.Euler(_movementProvider.Rotation.x, _movementProvider.Rotation.y, 0);
            transform1.position = camPosition.position;
        }

        private void LimitVelocity()
        {
            // limit the character velocity
            var velocity = _rigidbody.velocity;
            var flatVelocity = new Vector3(velocity.x, 0, velocity.z);
            if (!(flatVelocity.magnitude > _movementProvider.Speed)) return;
            var limited = flatVelocity.normalized * _movementProvider.Speed;
            _rigidbody.velocity = new Vector3(limited.x, _rigidbody.velocity.y, limited.z);
        }

        private void GroundCheck()
        {
            _grounded = Physics.Raycast(_transform.position, Vector3.down, 1.1f, groundLayer.value);
            // if grounded apply drag so we dont slide
            _rigidbody.drag = _grounded ? groundDrag : 0;
        }
    }
}
