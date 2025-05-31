using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rb;
    private bool _isJump = false;
    [SerializeField] private float _jumpForce = 3;
    private bool _isGrounded = false;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask _groundMask;
    
    void Start()
    {
        _groundMask = LayerMask.GetMask("Ground");
    }
    
    void Update()
    {
        CalculateJump();

    }

    private void CalculateJump()
    {
        _isGrounded = Physics2D.Raycast(_rb.position, Vector2.down,_groundCheckDistance, _groundMask);
        if (Input.GetKeyDown(KeyCode.W))
        {
            _isJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            if (_isJump)
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _isJump = false;
            }  
        }
    }
}
