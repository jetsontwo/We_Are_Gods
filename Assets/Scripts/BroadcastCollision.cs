using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BroadcastMessage("OnParentCollisionEnter2D", collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        BroadcastMessage("OnParentCollisionExit2D", collision);
    }
}
