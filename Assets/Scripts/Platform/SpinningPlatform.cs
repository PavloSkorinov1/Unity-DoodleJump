using Core.Actions;
using UnityEngine;

namespace Platform
{
    public class SpinningPlatform : SpinningAction
    {
        private PlatformEffector2D _platformEffector; 
        private float _currentWorldRotationZ = 0f;

        protected override void Start()
        {
            base.Start();
            Initialise();
        }

        new void Update()
        {
            base.Update();
            _currentWorldRotationZ = transform.eulerAngles.z;
            _platformEffector.rotationalOffset = -_currentWorldRotationZ;
        }

        private void Initialise()
        {
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
    }
}
