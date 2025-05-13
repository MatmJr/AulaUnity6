using Unity.VisualScripting;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool ground = false;
    [SerializeField] Transform groundcheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] SpriteRenderer spriteRenderer;

    private bool wasGrounded = true;

    void Move()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }

    void CheckGroundAndFlipDirection()
    {
        ground = Physics2D.Linecast(groundcheck.position, transform.position, groundLayer);

        if (!ground && wasGrounded)
        {
            speed *=-1;
        }

        wasGrounded = ground;
    }

    void UpdateSpriteDirection()
    {
        spriteRenderer.flipX = speed >= 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().Die();
        }
    }
    void FixedUpdate()
    {
        Move();
        CheckGroundAndFlipDirection();
        UpdateSpriteDirection();        
    }
}
