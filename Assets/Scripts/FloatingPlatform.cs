using UnityEngine;
using System.Collections;

public class FloatingPlatform : MonoBehaviour
{
    public float platformSpeed;
    public float distance;  //Distance from center before the platform turning around
    public enum Positions {Left, Center, Right}
    public Positions startPosition;

    private bool movingRight;
    private float lerper;
    private float lerpSpeed;
    private Vector3 rightLimit;
    private Vector3 leftLimit;

    void Start ()
    {
        lerpSpeed = 1 / (10 * platformSpeed); //Make platform speed easier to manage

        if (startPosition == Positions.Left)
        {
            rightLimit = transform.position + new Vector3(distance * 2, 0, 0);
            leftLimit = transform.position;

            lerper = 0f;
        }
        else if (startPosition == Positions.Center)
        {
            rightLimit = transform.position + new Vector3(distance, 0, 0);
            leftLimit = transform.position + new Vector3(-distance, 0, 0);

            lerper = 0.5f;
        }
        else if (startPosition == Positions.Right)
        {
            rightLimit = transform.position;
            leftLimit = transform.position + new Vector3(distance * -2, 0, 0);

            lerper = 1f;
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(leftLimit, rightLimit, lerper);

        //Turn around if hit edge
        if (lerper >= 1 && movingRight)
        {
            movingRight = false;
        }
        else if (lerper <= 0 && !movingRight)
        {
            movingRight = true;
        }

        //Lerp closer
        if (movingRight)
        {
            lerper += lerpSpeed;
        }
        else
        {
            lerper -= lerpSpeed;
        }
    }
}
