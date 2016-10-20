using UnityEngine;
using System.Collections;

public class ChangeSolid : MonoBehaviour, Mechanic_Interface
{
    public void AddGameComponent()
    {
        GetComponentInParent<Collider2D>().enabled = false;
    }

    public void RemoveGameComponent()
    {
        GetComponentInParent<Collider2D>().enabled = true;
    }
}
