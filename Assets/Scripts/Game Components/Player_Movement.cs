using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour, Mechanic_Interface
{
    public float speed, max_vel;

    private float horiz_move;
    private Rigidbody2D rb;
    private Animator ar;
    private CameraFollow camFollow;

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        camFollow = Camera.main.GetComponent<CameraFollow>();

        AddGameComponent();
    }

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

            if (rb.velocity.x < max_vel && horiz_move > 0)  //If moving right and not at max (positive/right) velocity
            {
                rb.velocity += new Vector2(horiz_move * speed * Time.deltaTime, 0);
            }
            else if (rb.velocity.x > -max_vel && horiz_move < 0)    //If moving left and not at max (negative/left) velocity
            {
                rb.velocity += new Vector2(horiz_move * speed * Time.deltaTime, 0);
            }
            //Otherwise weird behavior when switching directions at max velocity
        }
        else
        {
            rb.velocity -= new Vector2(rb.velocity.x, 0) * Time.deltaTime * 5;
            ar.SetBool("walk_left", false);
            ar.SetBool("walk_right", false);
        }

        if(Mathf.Abs(rb.velocity.x) <= 0.2f)
        {
            ar.SetBool("Idle", true);
        }
    }

    public void AddGameComponent()
    {
        //Finds the new Rigidbody2D to act upon and sets the position of the game component on top of the new Player or actable object
        rb = transform.parent.GetComponent<Rigidbody2D>();
        ar = transform.parent.GetComponent<Animator>();

        camFollow.followTrans = transform.parent;
    }

    public void RemoveGameComponent()
    {
        //Resets the velocity of the object to zero when the game component is removed
        rb.velocity = Vector2.zero;
    }
}
