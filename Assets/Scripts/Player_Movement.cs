using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

    public float speed;
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

        rb.AddForce(new Vector2(horiz_move * speed, 0));
        
    }
}
