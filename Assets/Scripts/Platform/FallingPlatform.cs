using System.Collections;
using UnityEngine;

namespace Platform
{
    public class FallingPlatform : MonoBehaviour
    {
        [SerializeField] private float fallDelay = 0.5f;
        [SerializeField] private float destroyDelay = 2f;

        private Rigidbody2D _rb;
        private Collider2D _platformCollider;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                enabled = false;
                return;
            }
            _platformCollider = GetComponent<Collider2D>();
            if (_platformCollider == null)
            {
                enabled = false;
                return;
            }
            
            _rb.bodyType = RigidbodyType2D.Static;
            _rb.gravityScale = 0f;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_rb.bodyType == RigidbodyType2D.Static && collision.gameObject.CompareTag("Player"))
            {
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb == null)
                {
                    return;
                }

                bool isLandingFromAbove = (playerRb.linearVelocity.y <= 0.1f) && (collision.transform.position.y > _platformCollider.bounds.center.y);

                if (isLandingFromAbove)
                {
                    _rb.bodyType = RigidbodyType2D.Kinematic;
                    StartCoroutine(FallAndDestroy());
                }
            }
        }
        
        private IEnumerator FallAndDestroy()
        {
            yield return new WaitForSeconds(fallDelay);
            if (_rb != null)
            {
                _rb.bodyType = RigidbodyType2D.Dynamic; 
                _rb.gravityScale = 1f;
            }
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
        }
    }
}