using UnityEngine;
using System.Collections;

public class Component_Transfer : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject componentToTransfer = Physics2D.OverlapPoint(new Vector2(mousePosition.x, mousePosition.y)).gameObject;

            if(componentToTransfer != null && componentToTransfer.CompareTag("Component"))
            {
                Transfer_Component(gameObject, componentToTransfer);
            }            
        }
	}

    void Transfer_Component(GameObject objectToTransferTo, GameObject componentToTransfer)
    {
        componentToTransfer.transform.SetParent(objectToTransferTo.transform, false);
        componentToTransfer.GetComponent<Mechanic_Interface>().Update_Parent();
    }
}
