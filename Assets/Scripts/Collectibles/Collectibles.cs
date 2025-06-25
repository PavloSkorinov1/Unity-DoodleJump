using Core.Manager;
using UI.Managers;
using UI.View;
using UnityEngine;

namespace Collectibles
{
    public class Collectibles : MonoBehaviour
    {
        [SerializeField] private AudioClip collectSound;
        private int _scoreValue = 1;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (collectSound != null)
                {
                    AudioSource.PlayClipAtPoint(collectSound, transform.position);
                }
                else
                {
                    Debug.LogWarning("Collectible: AudioClip is not assigned");
                }
                
                if (PlayerTracker.Instance != null)
                {
                    PlayerTracker.Instance.AddScore(_scoreValue);
                }
                else
                {
                    Debug.LogWarning("Collectible: PlayerTracker instance not found");
                }

                Destroy(gameObject);
            }
        }
    }
}
