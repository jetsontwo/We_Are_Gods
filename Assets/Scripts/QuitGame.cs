using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("You quit the game!");
#else
        Application.Quit();
#endif
    }
}
