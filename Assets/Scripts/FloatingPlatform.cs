using UnityEngine;
using System.Collections;

public class FloatingPlatform : MonoBehaviour
{
    public float platformSpeed;
    public float distance;  //Distance from center before the platform turning around

    private float lerper;
    private bool movingRight;
    private Vector3 rightLimit;
    private Vector3 leftLimit;

    void Start ()
    {
        rightLimit = transform.position + new Vector3(distance, 0, 0);
        leftLimit = transform.position + new Vector3(-distance, 0, 0);
    }

    void FixedUpdate()
    {
        float lerpSpeed = 1 / (10 * platformSpeed); //Make platform speed easier to manage
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
