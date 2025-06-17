using UnityEngine;
using System.Collections;

namespace Platform
{
    public class FallingPlatform : ActionBase
    {
        public float fallDelay = 0.5f;
        public float destroyDelay = 2f;

        private Rigidbody2D _rb;
        private Collider2D _platformCollider;
        
        protected override void Start()
        {
            base.Start();

            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                Debug.LogError("FallingPlatform: No Rigidbody2D found");
                enabled = false;
                return;
            }

            _platformCollider = GetComponent<Collider2D>();
            if (_platformCollider == null)
            {
                Debug.LogError("FallingPlatform: No Collider2D found");
                enabled = false;
                return;
            }
            
            _rb.bodyType = RigidbodyType2D.Static;
            _rb.gravityScale = 0f;
        }
        
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (executeOnCollision && (!executeOnce || !hasExecuted) && _rb.bodyType == RigidbodyType2D.Static)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (playerRb == null)
                    {
                        return;
                    }
                    
                    bool isLandingFromAbove = (playerRb.linearVelocity.y <= 0.1f) &&
                                              (collision.transform.position.y > _platformCollider.bounds.center.y);

                    if (isLandingFromAbove)
                    {
                        base.OnCollisionEnter2D(collision);
                        _rb.bodyType = RigidbodyType2D.Kinematic;
                    }
                }
            }
        }
        protected override void ExecuteAction()
        {
            StartCoroutine(FallAndDestroy());
        }
        private IEnumerator FallAndDestroy()
        {
            yield return new WaitForSeconds(fallDelay);
            if (_rb)
            {
                _rb.bodyType = RigidbodyType2D.Dynamic; 
                _rb.gravityScale = 1f;
            }
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
        }
    }
}