using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;

    private GameObject Camera;

    private float XPosition { get; set; }

    private float Length { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        Length = GetComponent<SpriteRenderer>().bounds.size.x;

        XPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMove = Camera.transform.position.x * parallaxEffect;
        transform.position = new Vector3(XPosition + distanceToMove, transform.position.y);

        float distanceMoved = Camera.transform.position.x * (1 - parallaxEffect);
        if (distanceMoved > XPosition + Length)
        {
            XPosition = XPosition + Length;
        }
        else if (distanceMoved < XPosition - Length)
        {
            XPosition = XPosition - Length;
        }
    }
}
