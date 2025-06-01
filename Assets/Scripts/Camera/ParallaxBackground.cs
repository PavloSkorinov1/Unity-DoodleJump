using UnityEngine;

namespace Camera
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [Range(0f, 1f)]
        [SerializeField] private float parallaxStrength = 0.5f; 

        private Vector3 lastCameraPosition;

        void Start()
        {
            if (UnityEngine.Camera.main != null) cameraTransform = UnityEngine.Camera.main.transform;
            if (cameraTransform == null)
            {
                Debug.LogError("ParallaxBackground: No camera assigned and no Main Camera found! Disabling script.");
                enabled = false;
                return;
            }
            lastCameraPosition = cameraTransform.position;
        }
        void LateUpdate()
        {
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * parallaxStrength, deltaMovement.y * parallaxStrength, 0);
            lastCameraPosition = cameraTransform.position;
        }
    }
}