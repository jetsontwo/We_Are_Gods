using UnityEngine;
using System.Collections;

public class Test_Script : MonoBehaviour {

    void put_script_into(GameObject go, GameObject child_script)
    {
        child_script.transform.SetParent(go.transform, false);
    }
}
