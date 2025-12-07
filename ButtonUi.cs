using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject Canvas;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Destroy(Canvas);
    }
}

