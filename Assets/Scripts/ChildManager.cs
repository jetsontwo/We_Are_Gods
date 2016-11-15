using UnityEngine;
using System.Collections;

public class ChildManager : MonoBehaviour
{
    GameObject[] children;

    public float radius;
    private Vector3 rotOffset;

    public bool showChildren;
    int childSize = 3;

    void Awake()
    {
        UpdateChildren();
    }

    public void UpdateChildren()
    {
        //Get all children
        int child_count = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Component"))
            {
                child_count++;
            }
        }
        children = new GameObject[child_count];
        int count = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).CompareTag("Component"))
            {
                children[count] = transform.GetChild(i).gameObject;
                count++;
            }
        }
    }

    public void ArrangeChildren()
    {
        if (showChildren)
        {
            //METHOD 1 - TRIG (NOT VERY GOOD)
            //Profiler.BeginSample("Trig method");
            float rot = (2 * Mathf.PI) / children.Length;

            for (int i = 0; i < children.Length; i++)
            {
                children[i].GetComponent<SpriteRenderer>().enabled = true;
                children[i].GetComponent<Collider2D>().enabled = true;

                float totalRot = (rot * i) + (Mathf.PI / 2);
                Vector3 trigRot = new Vector3(Mathf.Cos(totalRot) / transform.localScale.x, Mathf.Sin(totalRot) / transform.localScale.y, 0);

                children[i].transform.localPosition = trigRot * radius;
                children[i].transform.localScale = new Vector3(childSize / transform.localScale.x, childSize / transform.localScale.y,
                    childSize / transform.localScale.z);
            }
            //Profiler.EndSample();

            //METHOD 2 - FAKE TRANSFORM (ABOUT THE SAME)
            /*Profiler.BeginSample("Fake transform method");
            rotOffset = new Vector3(0, 0, 360 / children.Length);
            GameObject tempRot = new GameObject();  //Use this objects rotation for now because it seems the easiest

            for (int i = 0; i < children.Length; i++)
            {
                children[i].GetComponent<SpriteRenderer>().enabled = true;
                children[i].GetComponent<Collider2D>().enabled = true;

                Vector3 totalRot = rotOffset * i;

                tempRot.transform.position = transform.position;
                tempRot.transform.eulerAngles = totalRot;

                Ray childRay = new Ray(transform.position, tempRot.transform.up);   //If you want to improve this keep in mind that the ray's direction is normalized
                children[i].transform.position = childRay.GetPoint(radius);
            }
            Destroy(tempRot);
            Profiler.EndSample();*/
        }
        else
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i].GetComponent<SpriteRenderer>().enabled = false;
                children[i].GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void show_children ()
    {
        if (children.Length > 0)
        {
            showChildren = true;
            ArrangeChildren();
        }
    }

    public void no_show_children()
    {
        if (children.Length > 0)
        {
            showChildren = false;
            ArrangeChildren();
        }
    }
}
