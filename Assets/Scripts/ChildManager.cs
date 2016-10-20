using UnityEngine;
using System.Collections;

public class ChildManager : MonoBehaviour
{
    GameObject[] children;

    public float radius;
    private Vector3 rotOffset;
    bool showChildren;

    void Awake()
    {
        UpdateChildren();
    }

    public void UpdateChildren()
    {
        //Get all children
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
    }

    void ArrangeChildren ()
    {
        if (showChildren)
        {
            //METHOD 1 - TRIG (NOT VERY GOOD)
            float rot = (2 * Mathf.PI) / children.Length;

            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetActive(true);

                float totalRot = (rot * i) + (Mathf.PI / 2);
                Vector3 trigRot = new Vector3(Mathf.Cos(totalRot), Mathf.Sin(totalRot), 0);

                children[i].transform.localPosition = trigRot * radius;
            }

            //METHOD 2 - FAKE TRANSFORM (ABOUT THE SAME)
            /*rotOffset = new Vector3(0, 0, 360 / children.Length);
            GameObject tempRot = new GameObject();  //Use this objects rotation for now because it seems the easiest

            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetActive(true);

                Vector3 totalRot = rotOffset * i;

                tempRot.transform.position = transform.position;
                tempRot.transform.eulerAngles = totalRot;

                Ray childRay = new Ray(transform.position, tempRot.transform.up);   //If you want to improve this keep in mind that the ray's direction is normalized
                children[i].transform.position = childRay.GetPoint(radius);
            }
            Destroy(tempRot);*/
        }
        else
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetActive(false);
            }
        }
    }

    void OnMouseDown ()
    {
        showChildren = !showChildren;
        ArrangeChildren();
    }
}
