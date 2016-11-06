using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Vector2 minPoint;
    public Vector2 maxPoint;

    [HideInInspector]
    public Transform followTrans;
    
    void Start()
    {
        GetComponent<Camera>().orthographicSize = 2;
    }

    void Update()
    {
        Vector3 tempPos = new Vector3(followTrans.position.x, followTrans.position.y, -10);

        if (tempPos.x > maxPoint.x)
        {
            tempPos = new Vector3(maxPoint.x, tempPos.y, -10);
        }
        else if (tempPos.x < minPoint.x)
        {
            tempPos = new Vector3(minPoint.x, tempPos.y, -10);
        }

        if (tempPos.y > maxPoint.y)
        {
            tempPos = new Vector3(tempPos.x, maxPoint.y, -10);
        }
        else if (tempPos.y < minPoint.y)
        {
            tempPos = new Vector3(tempPos.x, minPoint.y, -10);
        }

        transform.position = tempPos;
    }
}
