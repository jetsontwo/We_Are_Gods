using UnityEngine;

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
        Vector3 tempPos = new Vector3(followTrans.position.x, followTrans.position.y, -10); //-10 just to be safe

        if (tempPos.x > maxPoint.x)
        {
            tempPos = new Vector3(maxPoint.x, tempPos.y, tempPos.z);
        }
        else if (tempPos.x < minPoint.x)
        {
            tempPos = new Vector3(minPoint.x, tempPos.y, tempPos.z);
        }

        if (tempPos.y > maxPoint.y)
        {
            tempPos = new Vector3(tempPos.x, maxPoint.y, tempPos.z);
        }
        else if (tempPos.y < minPoint.y)
        {
            tempPos = new Vector3(tempPos.x, minPoint.y, tempPos.z);
        }

        transform.position = tempPos;
    }
}
