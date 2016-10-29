using UnityEngine;
using System.Collections;

public class Component_Transfer : MonoBehaviour {

    private GameObject object_clicked_storage;
    private ChildManager cm, game_object_cm;
	// Update is called once per frame

    void Start()
    {
        cm = GetComponent<ChildManager>();
    }
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //Scans for a collider when the player clicks at the current mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D object_clicked = Physics2D.OverlapPoint(new Vector2(mousePosition.x, mousePosition.y));


            //Sees if the object caught by the raycast is nothing (option 1) a component to transfer (option 2) or a player or transferable Game Object (option 3)
            if (object_clicked == null)
            {
                if (object_clicked_storage != null)
                {
                    if (game_object_cm.showChildren)
                        game_object_cm.no_show_children();
                    object_clicked_storage = null;
                    game_object_cm = null;
                }
                if (cm.showChildren)
                    cm.no_show_children();
            }
            else if (object_clicked.CompareTag("Component"))
            {
                if (object_clicked.transform.parent != gameObject.transform)
                    Transfer_Component(gameObject, object_clicked.gameObject);
                else
                {
                    Transfer_Component(object_clicked_storage, object_clicked.gameObject);
                }
            }
            else if (object_clicked.CompareTag("Transfer"))
            {


                if (object_clicked_storage == object_clicked.gameObject)
                {
                    game_object_cm.no_show_children();
                    cm.no_show_children();
                }
                else
                {
                    if (game_object_cm)
                        game_object_cm.no_show_children();
                    object_clicked_storage = object_clicked.gameObject;
                    game_object_cm = object_clicked_storage.GetComponent<ChildManager>();
                    if (!game_object_cm.showChildren)
                        game_object_cm.show_children();
                    if (!cm.showChildren)
                        cm.show_children();
                }



            }
            else if (object_clicked.CompareTag("Player"))
            {
                if (game_object_cm)
                    if (game_object_cm.showChildren)
                        game_object_cm.no_show_children();
                if (cm.showChildren)
                    cm.no_show_children();
                else
                    cm.show_children();
            }
        }
	}

    void Transfer_Component(GameObject objectToTransferTo, GameObject componentToTransfer)
    {
        componentToTransfer.GetComponent<Mechanic_Interface>().RemoveGameComponent();

        Transform old_parent = componentToTransfer.transform.parent;
        //Sets the parent of the game component to the one specified
        componentToTransfer.transform.SetParent(objectToTransferTo.transform, false);

        //Updates the children of the object that had the component removed and that of the new parent
        old_parent.GetComponent<ChildManager>().UpdateChildren();
        componentToTransfer.transform.parent.GetComponent<ChildManager>().UpdateChildren();
        componentToTransfer.GetComponent<SpriteRenderer>().enabled = false;
        componentToTransfer.GetComponent<BoxCollider2D>().enabled = false;

        //Runs the functions to reset the game component as its moved between GameObjects
        componentToTransfer.GetComponent<Mechanic_Interface>().AddGameComponent();
        objectToTransferTo.GetComponent<ChildManager>().UpdateChildren();
        objectToTransferTo.GetComponent<ChildManager>().ArrangeChildren();
    }
}
