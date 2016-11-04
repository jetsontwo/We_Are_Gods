using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Level " + levelNum);
    }
}
