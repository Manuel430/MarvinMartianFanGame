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
        sceneManagement.SetLevel(0);
    }

    public void Restart(int currentScene)
    {
        sceneManagement.SetLevel(currentScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
