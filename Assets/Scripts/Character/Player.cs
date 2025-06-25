using UnityEngine;
using System.Collections.Generic;
using Core.Managers.UI;

namespace Character
{
    public class Player : MonoBehaviour
    {
        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float groundCheckDistance = 0.2f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private int maxJumps = 1;
        [Space]
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [Header("Audio Settings")]
        [SerializeField] private AudioClip jumpSound;
        
        private Rigidbody2D _rb;
        private Animator _anim;
        private AudioSource audioSource;
        // Jumping
        private bool _isJump = false;
        private float _horizontalInput;
        private int _jumpCount;
        // Screen Wrapping
        private UnityEngine.Camera _mainCamera; 
        private float _screenHalfWidth;
        private float _playerHalfWidth;
        // Game Over
        private float cameraBottomYThreshold;

        void Start()
        {
            Initialise();
        }

        void Update()
        {
            CalculateInput();
            CheckGameOverCondition();
        }

        private void LateUpdate()
        {
            CalculateMove();
            CalculateJump();
            CalculateAnimation();
            HandleScreenWrapping();
            
            if (IsGrounded())
            {
                _jumpCount = 0;
            }
        }

        private void Initialise()
        {
            groundMask = LayerMask.GetMask("Ground");
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            _mainCamera = UnityEngine.Camera.main;
            
            if (_mainCamera != null)
            {
                _screenHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
                cameraBottomYThreshold = _mainCamera.transform.position.y - _mainCamera.orthographicSize - 1.0f;
            }
            else
            {
                Debug.LogError("Player: Main Camera not found");
                enabled = false;
            }
            
            Collider2D playerCollider = GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                _playerHalfWidth = playerCollider.bounds.extents.x;
            }
            else
            {
                Debug.LogWarning("Player: No Collider2D found ");
                _playerHalfWidth = 0.5f;
            }
        }

        private void CalculateInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _isJump = true; 
            }
        }

        private void CalculateMove()
        {
            
            Vector2 movement = new Vector2(_horizontalInput * moveSpeed, _rb.linearVelocity.y);
            _rb.linearVelocity = movement;
        }

        private void CalculateJump()
        {
            if (_isJump && _jumpCount < maxJumps)
            {             
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                if (audioSource != null && jumpSound != null)
                {
                    audioSource.PlayOneShot(jumpSound);
                }
                _jumpCount++;
                _isJump = false;
            }
            else if (_isJump)
            {
                _isJump = false;
            }
        }

        private void CalculateAnimation()
        {
            _anim.SetFloat("Speed", Mathf.Abs(_horizontalInput));

            if (_horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            
            _anim.SetBool("IsGrounded", IsGrounded());
            _anim.SetFloat("VerticalSpeed", _rb.linearVelocity.y);
        }
        
        private void HandleScreenWrapping()
        {
            if (_mainCamera == null) return;

            float playerX = transform.position.x;
            float cameraX = _mainCamera.transform.position.x;
            
            float leftScreenEdge = cameraX - _screenHalfWidth;
            float rightScreenEdge = cameraX + _screenHalfWidth;

            if (playerX > rightScreenEdge + _playerHalfWidth)
            {
                transform.position = new Vector3(leftScreenEdge, transform.position.y, transform.position.z);
            }
            else if (playerX < leftScreenEdge - _playerHalfWidth)
            {
                transform.position = new Vector3(rightScreenEdge, transform.position.y, transform.position.z);
            }
        }
        
        private bool IsGrounded()
        {
            if (groundCheck == null) return false;
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
            return hit.collider != null;
        }
        
        private void CheckGameOverCondition()
        {
            if (_mainCamera == null) return;
            
            cameraBottomYThreshold = _mainCamera.transform.position.y - _mainCamera.orthographicSize - 1.0f;

            if (transform.position.y < cameraBottomYThreshold)
            {
                if (GameOverManager.Instance != null && !GameOverManager.Instance.IsGameOver())
                {
                    GameOverManager.Instance.ShowGameOver();
                }
            }
        }

    }
}