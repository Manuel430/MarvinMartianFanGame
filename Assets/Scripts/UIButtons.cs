using UnityEngine;

public class UIButtons : MonoBehaviour
{
    SceneManagement sceneManagement;
    [SerializeField] PlayerUI pause;

    private void Awake()
    {
        sceneManagement = gameObject.AddComponent<SceneManagement>();
    }
    public void Continue()
    {
        pause.SetUnpause();
    }

    public void MainMenu()
    {
        Debug.Log("PressingButton");
        sceneManagement.SetLevel(0);
        sceneManagement.NextScene();
    }

    public void Restart(int currentScene)
    {
        sceneManagement.SetLevel(currentScene);
        sceneManagement.NextScene();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
