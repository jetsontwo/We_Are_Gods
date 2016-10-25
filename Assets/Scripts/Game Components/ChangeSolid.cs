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
        transform.parent.GetComponent<Collider2D>().enabled = false;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void RemoveGameComponent()
    {
        transform.parent.GetComponent<Collider2D>().enabled = true;
    }
}
