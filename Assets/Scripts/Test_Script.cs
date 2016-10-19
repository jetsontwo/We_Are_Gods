using UnityEngine;
using System.Collections;

public class Test_Script : MonoBehaviour {

    public GameObject[] children;
    public GameObject ball;
    // Use this for initialization
    void Start() {
        children = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            children[i] = gameObject.transform.GetChild(i).gameObject;
        }

        put_script_into(ball, children[0]);

    }
    void put_script_into(GameObject go, GameObject child_script)
    {
        child_script.transform.SetParent(go.transform, false);
    }
}
