using UnityEngine;

namespace Platform
{
    public class SpinningPlatform : ActionBase
    {
        [Header("Spinning Platform Settings")]
        [SerializeField] private float spinSpeed = 60f; 
        [SerializeField] private Vector3 spinAxis = new Vector3(0, 0, 1); 

        private bool _isSpinning = false; 
        private PlatformEffector2D _platformEffector; 
        private float _currentWorldRotationZ = 0f;

        protected override void Start()
        {
            base.Start();
            var rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            _platformEffector = GetComponent<PlatformEffector2D>();
            if (_platformEffector == null)
            {
                Debug.LogError("SpinningPlatformAction: No PlatformEffector2D found on this GameObject", this);
                enabled = false;
                return;
            }
            _platformEffector.useOneWay = true;
            _platformEffector.surfaceArc = 180f;
        }

        void Update()
        {
            if (_isSpinning)
            {
                float rotationAmount = spinSpeed * Time.deltaTime;
                transform.Rotate(spinAxis, rotationAmount);
                _currentWorldRotationZ = transform.eulerAngles.z;
                _platformEffector.rotationalOffset = -_currentWorldRotationZ;
            }
        }
        protected override void ExecuteAction()
        {
            _isSpinning = true;
        }
    }
}
