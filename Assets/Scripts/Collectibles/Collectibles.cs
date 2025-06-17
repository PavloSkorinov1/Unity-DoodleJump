using Core.Manager;
using UnityEngine;

namespace Collectibles
{
    public class Collectibles : MonoBehaviour
    {
        private int _scoreValue = 1;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(_scoreValue);
            }
            else
            {
                Debug.LogWarning("Coin: GameManager instance not found");
            }
            
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
