using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour, Mechanic_Interface {

    public float speed, max_vel;
    private float horiz_move;
    private Rigidbody2D rb;

    void Start()
    {
        AddGameComponent();
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Horizontal"))
        {
            horiz_move = Input.GetAxis("Horizontal");
            if (rb.velocity.magnitude < max_vel)
            {
                rb.velocity += new Vector2(horiz_move * speed * Time.deltaTime, 0);
            }
        }
        else
        {
            rb.velocity -= new Vector2(rb.velocity.x, 0) * Time.deltaTime * 5;
        }
    }

    public void AddGameComponent()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        transform.localPosition = Vector3.zero;
    }

    public void RemoveGameComponent ()
    {

    }
}
