using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour, Mechanic_Interface
{

    public float speed, max_vel;
    private float horiz_move;
    private Rigidbody2D rb;
    private Animator ar;

    void Start()
    {
        AddGameComponent();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            horiz_move = Input.GetAxis("Horizontal");
            if (horiz_move > 0)
            {
                ar.SetBool("walk_right", true);
                ar.SetBool("walk_left", false);
                ar.SetBool("Idle", false);
            }
            else
            {
                ar.SetBool("walk_left", true);
                ar.SetBool("walk_right", false);
                ar.SetBool("Idle", false);
            }
            if (rb.velocity.magnitude < max_vel)
            {
                rb.velocity += new Vector2(horiz_move * speed * Time.deltaTime, 0);
            }

        }
        else
        {
            rb.velocity -= new Vector2(rb.velocity.x, 0) * Time.deltaTime * 5;
            ar.SetBool("walk_left", false);
            ar.SetBool("walk_right", false);
        }

        if(rb.velocity.magnitude <= 0.2f)
        {
            ar.SetBool("Idle", true);
        }
    }

    public void AddGameComponent()
    {
        //Finds the new Rigidbody2D to act upon and sets the position of the game component on top of the new Player or actable object
        rb = transform.parent.GetComponent<Rigidbody2D>();
        ar = transform.parent.GetComponent<Animator>();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void RemoveGameComponent()
    {
        //Resets the velocity of the object to zero when the game component is removed
        rb.velocity = Vector2.zero;
    }
}
