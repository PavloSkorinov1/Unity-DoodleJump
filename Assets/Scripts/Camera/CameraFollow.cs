using UnityEngine;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothTime;
        [SerializeField] private UnityEngine.Camera mainCamera;
        
        private float initialCameraX;


        private void Awake()
        {
            initialCameraX = transform.position.x;
            if (mainCamera == null)
            {
                Debug.LogError("CameraFollow: No Camera component found", this);
                enabled = false;
            }
        }

        private void FixedUpdate()
        {
            if (target == null || !target.gameObject.activeInHierarchy || mainCamera == null)
            {
                return;
            }

            float targetCameraY = Mathf.Max(transform.position.y, target.position.y);
            Vector3 targetPos = new Vector3(
                initialCameraX,
                Mathf.Lerp(transform.position.y, targetCameraY, smoothTime),
                transform.position.z
            );
            transform.position = targetPos;
        }
    }
}
