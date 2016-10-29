using UnityEngine;
using System.Collections;

public class NotSolid : MonoBehaviour, Mechanic_Interface
{
    public Color phasedColor;

    float restoreGrav;
    Color restoreColor;

    void Awake ()
    {
        AddGameComponent();
    }

    public void AddGameComponent()
    {
        if (!transform.parent.CompareTag("Player"))
        {
            if (transform.parent.GetComponent<Rigidbody2D>())
            {
                restoreGrav = transform.parent.GetComponent<Rigidbody2D>().gravityScale;
                transform.parent.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

            restoreColor = transform.parent.GetComponent<SpriteRenderer>().color;
            transform.parent.GetComponent<SpriteRenderer>().color = new Color(restoreColor.r, restoreColor.g, restoreColor.b, phasedColor.a);

            transform.parent.GetComponent<Collider2D>().isTrigger = true;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void RemoveGameComponent()
    {
        if (!transform.parent.CompareTag("Player"))
        {
            if (transform.parent.GetComponent<Rigidbody2D>())
            {
                transform.parent.GetComponent<Rigidbody2D>().gravityScale = restoreGrav;
            }

            transform.parent.GetComponent<SpriteRenderer>().color = restoreColor;

            transform.parent.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
