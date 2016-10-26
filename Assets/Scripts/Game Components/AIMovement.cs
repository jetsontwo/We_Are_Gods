using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour,Mechanic_Interface
{
    public float speed;
    public float maxSpeed;

    Rigidbody2D rb;
    RaycastHit2D hit;

    void Awake ()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        AddGameComponent();
    }

    void Update()
    {
        hit = Physics2D.Raycast(rb.transform.position, rb.transform.right, (rb.GetComponent<Collider2D>().bounds.extents.x + 0.01f));
        if (hit)
        {
            //Debug.Log(transform.parent.name + " hit a thing.");
            rb.transform.Rotate(0, 180, 0);
        }

        if (rb.transform.eulerAngles.y < 181 && rb.transform.eulerAngles.y > 179)
        {
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, 180, rb.transform.eulerAngles.z);

            if (rb.velocity.x < maxSpeed)  //If moving right and not at max (positive/right) velocity
            {
                rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
            }
        }
        else if(rb.transform.eulerAngles.y < 1 && rb.transform.eulerAngles.y > -1)
        {
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, 0, rb.transform.eulerAngles.z);

            if (rb.velocity.x > -maxSpeed )    //If moving left and not at max (negative/left) velocity
            {
                rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogError("Invlaid rotation of " + rb.transform.eulerAngles.y + " for AI Movement on " + rb.name + "!");
        }
        //Velocity checks are different otherwise weird behavior when switching directions at max velocity
        //Also, angle is set everytime because transform.Rotate can get weird numbers, like 1.035947e^-5, as angels sometimes
    }

    public void AddGameComponent()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void RemoveGameComponent()
    {

    }
}