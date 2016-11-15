using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public bool actuallyExit;

    void Update()
    {
        if (actuallyExit)
        {
            //Debug.Log("Loading next level.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (CompareTag("Player") && col.CompareTag("Exit"))
        {
            GetComponent<Animator>().SetTrigger("Exit Level");
        }
    }
}
