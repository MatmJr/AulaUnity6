using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb; 
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    private float horizontal;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private int movingHash = Animator.StringToHash("Moving");
    private int jumpingHash = Animator.StringToHash("Jumping");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #region MOVE_CONTROLLS
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
    }
    #endregion

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        animator.SetBool(movingHash, horizontal!=0);
        animator.SetBool(jumpingHash, !IsGrounded());

        if(horizontal > 0){
            spriteRenderer.flipX = false;
        }else {
            spriteRenderer.flipX = true;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(
            groundCheck.position, 
            new Vector2(0.8f,0.1f), 
            CapsuleDirection2D.Horizontal, 
            0, 
            groundLayer
        );
    }
}