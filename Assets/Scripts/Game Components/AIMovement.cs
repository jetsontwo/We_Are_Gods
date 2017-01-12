using UnityEngine;

public class AIMovement : MonoBehaviour, Mechanic_Interface
{
    //Doesn't like slopes

    float speed;
    public float maxSpeed;

    float forwardEdge;
    Rigidbody2D rb;
    RaycastHit2D hit;

    public bool testEdge;
    public GameObject edgeIndicator;

    Rigidbody2D playerRb;

    void Awake ()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (testEdge)
        {
            edgeIndicator = (GameObject)Instantiate(Resources.Load("Simple Dot"));
        }

        AddGameComponent();
    }

    void Update()
    {
        if (testEdge)
        {
            Ray direction = new Ray(rb.transform.position, rb.transform.right);
            edgeIndicator.transform.position = direction.GetPoint(forwardEdge * 1.1f);
        }

        hit = Physics2D.Raycast(rb.transform.position, rb.transform.right, forwardEdge * 1.1f);
        if (hit)
        {
            //Debug.Log(transform.parent.name + " hit a thing.");
            rb.transform.Rotate(0, 180, 0);
        }

        if (rb.transform.eulerAngles.y < 181 && rb.transform.eulerAngles.y > 179)   //Facing left
        {
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, 180, rb.transform.eulerAngles.z);
            if (playerRb != null)
            {
                playerRb.transform.eulerAngles = new Vector3(playerRb.transform.eulerAngles.x, 0, playerRb.transform.eulerAngles.z);
            }

            if (rb.velocity.x > -maxSpeed)  //If moving left and not at max (negative/left)  velocity
            {
                Debug.Log("Added force to thing");
                rb.AddForce(new Vector2(-speed / rb.mass, 0), ForceMode2D.Impulse);
            }
        }
        else if(rb.transform.eulerAngles.y < 1 && rb.transform.eulerAngles.y > -1)  //Facing right
        {
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, 0, rb.transform.eulerAngles.z);
            if (playerRb != null)
            {
                playerRb.transform.eulerAngles = new Vector3(playerRb.transform.eulerAngles.x, 0, playerRb.transform.eulerAngles.z);
            }

            if (rb.velocity.x < maxSpeed)    //If moving right and not at max (positive/right) velocity
            {
                Debug.Log("Added force to thing");
                rb.AddForce(new Vector2(speed / rb.mass, 0), ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogError("Invlaid rotation of " + rb.transform.eulerAngles.y + " for AI Movement on " + rb.name + "!");
        }
        //Velocity checks are different otherwise weird behavior when switching directions at max velocity
        //Also, angle is set everytime because transform.Rotate can get weird numbers, like 1.035947e^-5, as angels sometimes


        if (playerRb != null)
        {
            Debug.Log("Added " + (2 * speed * playerRb.mass) + " impulse to player");

            if (rb.transform.eulerAngles.y < 181 && rb.transform.eulerAngles.y > 179)   //Facing left
            {
                playerRb.AddForce(new Vector2(-4 * speed * playerRb.mass, 0), ForceMode2D.Impulse);
            }
            else if (rb.transform.eulerAngles.y < 1 && rb.transform.eulerAngles.y > -1)   //Facing right
            {
                playerRb.AddForce(new Vector2(4 * speed * playerRb.mass, 0), ForceMode2D.Impulse);
            }
        }
    }
    private void OnParentCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerRb = collision.collider.GetComponent<Rigidbody2D>();
        }
    }

    private void OnParentCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerRb = null;
        }
    }

    public void AddGameComponent()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        forwardEdge = rb.GetComponent<Collider2D>().bounds.extents.x + (rb.GetComponent<Collider2D>().offset.x * rb.transform.localScale.x);

        Character_Stats cs = transform.parent.GetComponent<Character_Stats>();
        speed = cs.speed;
    }

    public void RemoveGameComponent()
    {
        rb.velocity = Vector2.zero;
    }
}