using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    private bool isDead = false;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    #region MOVE_CONTROLLS
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded() && isDead==false)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
    }
    #endregion

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
    void ApplyMovement()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    void UpdateAnimation()
    {
        animator.SetBool(movingHash, horizontal!=0);
        animator.SetBool(jumpingHash, !IsGrounded());
    }

    void UpdateSpriteOrientation()
    {
        if(horizontal > 0){
            spriteRenderer.flipX = false;
        }else if(horizontal<0){
            spriteRenderer.flipX = true;
        }
    }


    public void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        capsuleCollider2D.enabled = false;

        animator.Rebind();
        animator.Update(0f);

        spriteRenderer.color = new Color(0.5f, 0f, 0f, 0.5f);
        transform.rotation = Quaternion.Euler(0,0,-90);

        Invoke("LoadScene", 0.5f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag=="Enemy")
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up*15, ForceMode2D.Impulse);
            collider.gameObject.GetComponent<SpriteRenderer>().flipY=true;
            collider.gameObject.GetComponent<EnemyScript>().enabled=false;
            collider.gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
            collider.gameObject.GetComponent<BoxCollider2D>().enabled=false;
            collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collider.gameObject, 0.5f);

        }
    }
    private void FixedUpdate()
    {
        if (isDead) return;

        ApplyMovement();
        UpdateAnimation();
        UpdateSpriteOrientation();
    }
}