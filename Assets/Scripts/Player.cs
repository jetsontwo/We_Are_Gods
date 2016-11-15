using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool actuallyExit;

    void Update()
    {
        if (actuallyExit)
        {
            //Debug.Log("Loading next level.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetButtonDown("Reset"))
        {
            ResetLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (CompareTag("Player") && col.CompareTag("Exit"))
        {
            GetComponent<Animator>().SetTrigger("Exit Level");
        }

        if (col.CompareTag("Death"))
        {
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
