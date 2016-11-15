using UnityEngine;
using System.Collections;

public class FinalButton : MonoBehaviour
{
    public bool quitNow;

    void OnMouseDown()
    {
        GetComponent<Animator>().SetTrigger("Press");
    }

    void Update()
    {
        if (quitNow)
        {
            EndGame();
        }
    }

    void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("You ended the game!");
#else
        Application.Quit();
#endif
    }
}
