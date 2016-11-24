using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed;
    public Vector2 minMaxY; //X is min, Y is max
    public int standTime;
    public float shakeIntensity;
    
    private bool standingOn;
    private float minX;
    private float maxX;
    private float lerper = 0.5f;
    private bool movingRight;

    void Start()
    {
        minX = transform.position.x - shakeIntensity;
        maxX = transform.position.x + shakeIntensity;
    }

    void Update()
    {
        //Maybe add?
        //lerpSpeed = 1 / (10 * scrollSpeed); //Make platform speed easier to manage
        if (transform.position.y < minMaxY.y)
        {
            transform.Translate(0, scrollSpeed, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, minMaxY.x, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        if (standingOn)
        {
            Vector3 shakeRightSide = new Vector3(maxX, transform.position.y, transform.position.z);
            Vector3 shakeLeftSide = new Vector3(minX, transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(shakeLeftSide, shakeRightSide, lerper);

            //Turn around if hit edge
            if (lerper >= 1 && movingRight)
            {
                movingRight = false;
            }
            else if (lerper <= 0 && !movingRight)
            {
                movingRight = true;
            }

            //Lerp closer
            if (movingRight)
            {
                lerper += shakeIntensity;
            }
            else
            {
                lerper -= shakeIntensity;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && (col.collider.bounds.center.y - col.collider.bounds.extents.y) >= transform.position.y)
        {
            Invoke("Break", standTime);
            standingOn = true;
        }
    }

    void Break()
    {
        transform.GetComponent<Collider2D>().enabled = false;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.4f);

        standingOn = false;
        Vector3 shakeRightSide = new Vector3(maxX, transform.position.y, transform.position.z);
        Vector3 shakeLeftSide = new Vector3(minX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(shakeLeftSide, shakeRightSide, 0.5f);
    }
}
