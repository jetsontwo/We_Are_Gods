﻿using UnityEngine;
using System.Collections;

public class ChildManager : MonoBehaviour
{
    GameObject[] children;

    public float radius;
    private Vector3 rotOffset;

    void Awake()
    {
        UpdateChildren();
    }

    public void UpdateChildren()
    {
        //Get all children
        children = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            children[i] = gameObject.transform.GetChild(i).gameObject;
        }

        //Arrange children in circle

        //METHOD 1 - TRIG (NOT VERY GOOD)
        float rot = (2 * Mathf.PI) / children.Length;

        for (int i = 0; i < children.Length; i++)
        {
            float totalRot = (rot * i) + (Mathf.PI / 2);
            Vector3 trigRot = new Vector3(Mathf.Cos(totalRot), Mathf.Sin(totalRot), 0);
            
            children[i].transform.position = trigRot * radius;
        }

        //METHOD 2 - FAKE TRANSFORM (ABOUT THE SAME)
        /*rotOffset = new Vector3(0, 0, 360 / children.Length);
        GameObject tempRot = new GameObject();  //Use this objects rotation for now because it seems the easiest

        for (int i = 0; i < children.Length; i++)
        {
            Vector3 totalRot = rotOffset * i;

            tempRot.transform.position = transform.position;
            tempRot.transform.eulerAngles = totalRot;

            Ray childRay = new Ray(transform.position, tempRot.transform.up);   //If you want to improve this keep in mind that the ray's direction is normalized
            children[i].transform.position = childRay.GetPoint(radius);
        }
        Destroy(tempRot);*/
    }

    void OnMouseDown ()
    {
        Debug.Log("HI");
    }
}