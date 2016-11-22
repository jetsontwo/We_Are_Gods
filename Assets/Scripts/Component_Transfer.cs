using UnityEngine;
using System.Collections;

public class Component_Transfer : MonoBehaviour {

    private GameObject object_clicked_storage, component_transfer, transfer_location;
    private ChildManager cm, game_object_cm;
    private Transform before, after;
    private Rigidbody2D mechanic_rb;
    private bool moving_component = false;
    private ParticleSystem ps;
    //public Text_Manager tm;
	// Update is called once per frame

    void Start()
    {
        cm = GetComponent<ChildManager>();
    }
	void Update () {
        //tm.transferred_component = null;
        if (Input.GetMouseButtonDown(0) && !moving_component)
        {
            //Scans for a collider when the player clicks at the current mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D object_clicked = Physics2D.OverlapPoint(new Vector2(mousePosition.x, mousePosition.y));


            //Sees if the object caught by the raycast is nothing (option 1) a component to transfer (option 2)or transferable Game Object (option 3) or a player (option 4)
            if (object_clicked == null)
            {
                if (object_clicked_storage != null)
                {
                    Change_Emission(10);
                    ps = null;
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
                {
                    Transfer_Component(gameObject, object_clicked.gameObject);
                }
                else
                {
                    Character_Stats cs = object_clicked_storage.GetComponent<Character_Stats>();
                    for (int i = 0; i < cs.allowed_mechanics.Length; i++)
                    {
                        if (object_clicked.name.Equals(cs.allowed_mechanics[i]))
                        {
                            //tm.transferred_component = object_clicked.gameObject;

                            Transfer_Component(object_clicked_storage, object_clicked.gameObject);
                            //
                            //   SEND A MESSAGE HERE SAYING THEY CANNOT TRANSFER COMPONENT
                            //
                        }
                    }
                }
            }
            else if (object_clicked.CompareTag("Transfer"))
            {

                if (object_clicked_storage == object_clicked.gameObject)
                {
                    game_object_cm.no_show_children();
                    cm.no_show_children();
                    object_clicked_storage = null;

                    Change_Emission(10);
                    ps = null;

                }
                else
                {
                    if (game_object_cm)
                        game_object_cm.no_show_children();
                    object_clicked_storage = object_clicked.gameObject;
                    game_object_cm = object_clicked_storage.GetComponent<ChildManager>();
                    ps = object_clicked.GetComponentInChildren<ParticleSystem>();
                    Change_Emission(250);
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

                object_clicked_storage = null;
                game_object_cm = null;
            }
        }
        else if (moving_component)
        {
            float difx = before.position.x - after.position.x;
            float dify = before.position.y - after.position.y;

            if(Mathf.Abs(difx) >= 0.1 || Mathf.Abs(dify) >= 0.1)
            {
                mechanic_rb.velocity = new Vector2(-difx *2, -dify *2);
            }
            else
            {
                mechanic_rb.velocity = Vector2.zero;
                moving_component = false;
                Finish_Transfer();
            }
        }
        //tm.cur_touched_object = object_clicked_storage;
    }



    void Transfer_Component(GameObject objectToTransferTo, GameObject componentToTransfer)
    {
        component_transfer = componentToTransfer;
        transfer_location = objectToTransferTo;
        componentToTransfer.GetComponent<Mechanic_Interface>().RemoveGameComponent();

        Transform old_parent = componentToTransfer.transform.parent;
        //Sets the parent of the game component to the one specified
        componentToTransfer.transform.SetParent(null);
        old_parent.GetComponent<ChildManager>().UpdateChildren();
        //Do movement thing here
        Move_Component(componentToTransfer.transform, objectToTransferTo.transform, componentToTransfer.GetComponent<Rigidbody2D>());

        //Updates the children of the object that had the component removed and that of the new parent

    }

    void Move_Component(Transform component, Transform new_parent, Rigidbody2D object_to_move_rb)
    {
        before = component;
        after = new_parent;
        mechanic_rb = object_to_move_rb;
        moving_component = true;
    }

    void Finish_Transfer()
    {

        component_transfer.transform.SetParent(transfer_location.transform);
        component_transfer.transform.parent.GetComponent<ChildManager>().UpdateChildren();
        component_transfer.GetComponent<SpriteRenderer>().enabled = false;
        component_transfer.GetComponent<BoxCollider2D>().enabled = false;

        //Runs the functions to reset the game component as its moved between GameObjects
        component_transfer.GetComponent<Mechanic_Interface>().AddGameComponent();
        transfer_location.GetComponent<ChildManager>().UpdateChildren();
        transfer_location.GetComponent<ChildManager>().ArrangeChildren();
    }

    void Change_Emission(float value)
    {
        if(ps)
        {
            var em = ps.emission;
            var rate = new ParticleSystem.MinMaxCurve(value);
            em.rate = rate;
        }
    }
}
