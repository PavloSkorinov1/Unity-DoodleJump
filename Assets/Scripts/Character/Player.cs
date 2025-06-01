using UnityEngine;

namespace Character
{
    public class Player : MonoBehaviour
    {

        private Rigidbody2D _rb;
        private Animator _anim;
        private bool _isJump = false;
        private float _horizontalInput;
        
        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 3;
        [SerializeField] private float groundCheckDistance = 0.2f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheck;
        
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;

        void Start()
        {
            groundMask = LayerMask.GetMask("Ground");
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        void Update()
        {
            CalculateInput();
        }

        private void FixedUpdate()
        {
            CalculateMove();
            CalculateJump();
            CalculateAnimation();
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
            if (_isJump && IsGrounded())
            {                
                _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
                transform.localScale = new Vector3(3, 3, 1);
            }
            else if (_horizontalInput < 0)
            {
                transform.localScale = new Vector3(-3, 3, 1);
            }
            
            _anim.SetBool("IsGrounded", IsGrounded());
            _anim.SetFloat("VerticalSpeed", _rb.linearVelocity.y);
        }
        
        private bool IsGrounded()
        {
            if (groundCheck == null) return false;
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
            return hit.collider != null;
        }
    }
}