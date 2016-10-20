using UnityEngine;
using System.Collections;

public class Test_Script : MonoBehaviour {

    public GameObject to_put_into, child;
    void Start()
    {
        put_script_into(to_put_into, child);
    }

    void put_script_into(GameObject go, GameObject child_script)
    {
        child_script.transform.SetParent(go.transform, false);
        child_script.GetComponent<Mechanic_Interface>().Update_Parent();
    }
}
