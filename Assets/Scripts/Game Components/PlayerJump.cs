using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour, Mechanic_Interface
{
    public float jumpSpeed;
    bool jumping = false;
    private Animator am;
    public bool always_jump;
    public LayerMask includedLayers;

    Rigidbody2D rb;

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
            hit = Physics2D.Raycast(rb.transform.position, -rb.transform.up,
                ((rb.GetComponent<Collider2D>().bounds.extents.y - (rb.GetComponent<Collider2D>().offset.y * rb.transform.localScale.y)) * 1.1f),
                includedLayers);

            if (hit)
            {
                if (!hit.collider.isTrigger)  //This nested 'if', rather than an &&, avoids error messages if hit is null
                {
                    if(rb.velocity.y == 0)
                    {
                        rb.AddRelativeForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                    }
                }
            }
            jumping = false;
        }
    }

    public void AddGameComponent()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        if(transform.parent.CompareTag("Player"))
            am = transform.parent.GetComponent<Animator>();
    }

    public void RemoveGameComponent()
    {
        jumping = false;
        always_jump = false;
    }
}
