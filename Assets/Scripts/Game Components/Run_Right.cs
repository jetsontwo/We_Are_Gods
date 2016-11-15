using UnityEngine;
using System.Collections;

public class Run_Right : MonoBehaviour, Mechanic_Interface
{

    public float speed, max_vel;
    private float horiz_move;
    private Rigidbody2D rb;
    private Animator ar;
    private bool player;

    void Start()
    {
        AddGameComponent();
        player = false;
    }

    // Update is called once per frame
    void Update () {
        if (!player)
        {
            if (rb.velocity.x < max_vel && rb.transform.position.x < 2)
            {
                rb.velocity += new Vector2(speed * Time.deltaTime, 0);
                ar.SetBool("walking", true);
            }
            else if (rb.velocity.x > 0)
            {
                rb.velocity -= new Vector2(speed * Time.deltaTime * 5, 0);
                ar.SetBool("walking", true);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                ar.SetBool("walking", false);
            }
        }



	}


    public void AddGameComponent()
    {
        //Finds the new Rigidbody2D to act upon and sets the position of the game component on top of the new Player or actable object
        if(!transform.parent.CompareTag("Player"))
        {
            rb = transform.parent.GetComponent<Rigidbody2D>();
            ar = transform.parent.GetComponent<Animator>();
            Character_Stats cs = transform.parent.GetComponent<Character_Stats>();
            speed = cs.speed;
            player = false;
        }
        else
        {
            player = true;
            rb = null;
            ar = null;
        }
    }

    public void RemoveGameComponent()
    {
        //Resets the velocity of the object to zero when the game component is removed
        if(!player)
        {
            rb.velocity = Vector2.zero;
            ar.SetBool("walking", false);
        }
        player = false;
    }
}
