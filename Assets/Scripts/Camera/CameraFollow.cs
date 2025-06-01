using UnityEngine;

namespace Core.Manager
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothTime;
        [Space] 
        [SerializeField] private bool followX;
        [SerializeField] private bool followY;
        [SerializeField] private bool followZ;

        private void Update()
        {
            var targetPos = new Vector3(
                followX ? target.position.x : transform.position.x,
                followY ? target.position.y : transform.position.y,
                followZ ? target.position.z : transform.position.z
            );
                        
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothTime);
        }
    }
}
