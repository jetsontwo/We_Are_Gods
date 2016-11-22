using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
