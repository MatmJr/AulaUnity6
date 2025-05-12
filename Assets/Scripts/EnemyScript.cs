using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool ground = false;
    [SerializeField] Transform groundcheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] SpriteRenderer spriteRenderer;


    void FixedUpdate()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
        ground = Physics2D.Linecast(groundcheck.position, transform.position, groundLayer);
        Debug.Log(ground);

        if (ground==false)
        {
            speed *=-1;
        }

        if (speed>=0)
        {
            spriteRenderer.flipX = true;
        } else if (speed<0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
