using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour, Mechanic_Interface
{
    public float jumpSpeed;
    public bool always_jump;
    public LayerMask includedLayers;

    bool jumping = false;
    //bool onPlatform = false;
    float lowerEdge;

    private Rigidbody2D rb;
    //private Joint2D joint;
    private Animator am;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        AddGameComponent();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || always_jump)
        {
            jumping = true;
        }
        if(am)
            am.SetBool("Jump", jumping);

    }

    void FixedUpdate()
    {
        if (jumping)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(rb.transform.position, -rb.transform.up, lowerEdge * 1.1f, includedLayers);

            if (rb.velocity.y > -1 && rb.velocity.y < 1) //near 0 because it isn't always exact
            {
                if (hit)
                {
                    if (!hit.collider.isTrigger)  //This nested 'if', rather than an &&, avoids error messages if hit is null
                    {
                        /*if (onPlatform && transform.parent.parent != null)
                        {
                            Debug.Log("Remove plat");
                            onPlatform = false;
                            //joint.enabled = false;
                            //joint.connectedBody = null;
                            transform.parent.SetParent(null);
                        }*/
                        
                        rb.AddRelativeForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                    }
                }
            }
            jumping = false;
        }/*
        else if (!onPlatform && transform.parent.parent == null)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(rb.transform.position, -rb.transform.up, lowerEdge * 1.1f, includedLayers);

            if (hit)
            {
                if (!hit.collider.isTrigger && !hit.collider.CompareTag("Ground"))  //This nested 'if', rather than an &&, avoids error messages if hit is null
                {
                    onPlatform = true;
                    //joint.enabled = true;
                    //joint.connectedBody = hit.rigidbody;
                    transform.parent.SetParent(hit.transform);
                }
            }
        }/*
        else if (onPlatform)
        {
            if (Mathf.Abs(rb.velocity.x) < transform.parent.parent.GetComponent<Rigidbody2D>().velocity.x)
            {
                Debug.Log("Add parent vel");
                if (rb.velocity.x < 0)
                {
                    rb.velocity = 
                }
                else
                {

                }
            }
        }*/
    }

    public void AddGameComponent()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        //joint = transform.parent.GetComponent<Joint2D>();
        if (transform.parent.CompareTag("Player"))
            am = transform.parent.GetComponent<Animator>();
        lowerEdge = rb.GetComponent<Collider2D>().bounds.extents.y - (rb.GetComponent<Collider2D>().offset.y * rb.transform.localScale.y);
    }

    public void RemoveGameComponent()
    {
        jumping = false;
        always_jump = false;
    }
}
