using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPos;
    private Transform cam;
    [SerializeField] float parallaxEffect;

    void Start()
    {
       startPos = transform.position.x;
       length = GetComponent<SpriteRenderer>().bounds.size.x;
       cam = Camera.main.transform; 
    }

    void Update()
    {
        float rePos = cam.transform.position.x*(1-parallaxEffect);
        float distance = cam.transform.position.x*parallaxEffect;
        transform.position = new Vector3(
            startPos + distance, 
            transform.position.y,
            transform.position.z
        );
        if(rePos> startPos+length)
        {
            startPos += length;
        }else if(rePos< startPos-length)
        {
            startPos -= length;
        }

    }
}
