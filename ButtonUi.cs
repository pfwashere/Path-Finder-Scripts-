using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject Canvas;
    // Call this function to change the scene
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Destroy(Canvas);
    }
}
