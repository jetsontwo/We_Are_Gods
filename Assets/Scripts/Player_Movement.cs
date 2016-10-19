using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

    public float speed, max_vel;
    private int horiz_move;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.D))
        {
            horiz_move = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horiz_move = -1;
        }
        else
        {
            horiz_move = 0;
        }

        if(horiz_move != 0 && rb.velocity.magnitude < max_vel)
        {
            rb.velocity += new Vector2(horiz_move * speed * Time.deltaTime, 0);
        }
        else
        {
            rb.velocity -= new Vector2(rb.velocity.x, 0) * Time.deltaTime * 5;
        }
        
    }
}
