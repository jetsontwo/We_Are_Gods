using UnityEngine;
using System.Collections;

public class Component_Transfer : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //Scans for a collider when the player clicks at the current mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D componentToTransfer = Physics2D.OverlapPoint(new Vector2(mousePosition.x, mousePosition.y));

            if(componentToTransfer != null && componentToTransfer.CompareTag("Component"))
            {
                Transfer_Component(gameObject, componentToTransfer.gameObject);
            }            
        }
	}

    void Transfer_Component(GameObject objectToTransferTo, GameObject componentToTransfer)
    {
        //Sets the parent of the game component to the one specified
        componentToTransfer.transform.SetParent(objectToTransferTo.transform, false);

        //Runs the functions to reset the game component as its moved between GameObjects
        componentToTransfer.GetComponent<Mechanic_Interface>().RemoveGameComponent();
        componentToTransfer.GetComponent<Mechanic_Interface>().AddGameComponent();
    }
}
