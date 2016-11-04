using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
