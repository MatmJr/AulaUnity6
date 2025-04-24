using UnityEditor.Tilemaps;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int speed = 100;
    public int jumpSpeed = 100;

    public SpriteRenderer sprite ;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Move()
    {
       Vector3 move = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, Input.GetAxis("Jump") * Time.deltaTime * jumpSpeed, 0f);
    //    if (Input.GetAxis("Horizontal") > 0f){
    //     transform.eulerAngles = new Vector3(0f,0f,0f);
    //    }
    //    if (Input.GetAxis("Horizontal") < 0f){
    //     transform.eulerAngles = new Vector3(0f,180f,0f);
    //    }
        if (Input.GetAxis("Horizontal") > 0f){
            sprite.flipX = false;
        }
        else{
            sprite.flipX = true;
        }
       transform.position += move ;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
