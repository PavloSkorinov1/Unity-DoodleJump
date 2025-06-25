using UnityEngine;

namespace Core.Managers.Audio
{
    [RequireComponent(typeof(AudioSource))] 
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        
            [SerializeField] private AudioClip buttonClickSound;
            [SerializeField] private AudioSource _audioSource;
        
            void Awake()
            {
                if (Instance == null)
                {
                    Instance = this;
                }
                else
                {
                    return;
                }

                if (_audioSource == null)
                {
                    Debug.LogError("AudioManager: AudioSource component not found", this);
                    enabled = false;
                }

                _audioSource.playOnAwake = false; 
                _audioSource.loop = false;   
                _audioSource.spatialBlend = 0f;  
            }
            public void PlayButtonClickSound()
            {
                if (_audioSource != null && buttonClickSound != null)
                {
                    _audioSource.PlayOneShot(buttonClickSound);
                }
                else
                {
                    if (_audioSource == null)
                    {
                        Debug.LogWarning("AudioManager: AudioSource is null");
                    }
                    else if (buttonClickSound == null)
                    {
                        Debug.LogWarning("AudioManager: AudioClip is not assigned!");
                    }
                }
            }
    }
}
