using UnityEngine;
using System.Collections;

public class Test_Script : MonoBehaviour {

    public GameObject to_put_into, child;
    void Start()
    {
        put_script_into(to_put_into, child);
    }

    void put_script_into(GameObject go, GameObject child_holding_script)
    {
        child_holding_script.transform.SetParent(go.transform, false);
        child_holding_script.GetComponent<Mechanic_Interface>().Update_Parent();
    }
}
