using UnityEngine;
using System.Collections;

public class ChangeSolid : MonoBehaviour, Mechanic_Interface
{
    void Awake ()
    {
        AddGameComponent();
    }

    public void AddGameComponent()
    {
        GetComponentInParent<Collider2D>().enabled = false;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void RemoveGameComponent()
    {
        GetComponentInParent<Collider2D>().enabled = true;
    }
}
