﻿using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour, Mechanic_Interface
{
    public float jumpSpeed;
    public float playerHeight;
    bool jumping = false;

    Rigidbody2D rb;

    void Start()
    {
        AddGameComponent();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }
    }

    void FixedUpdate()
    {
        if (jumping)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(rb.transform.position, -rb.transform.up, (rb.GetComponent<Collider2D>().bounds.extents.y + 0.01f));

            if (hit)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    rb.AddRelativeForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                    jumping = false;
                }
            }
        }
    }

    public void AddGameComponent()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        transform.localPosition = Vector3.zero;
    }

    public void RemoveGameComponent()
    {

    }
}
