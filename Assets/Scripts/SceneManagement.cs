using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] int level;

    public void SetLevel(int levelChoice)
    {
        level = levelChoice;
    }

    public void NextScene()
    {
        StartCoroutine(LoadingScene(level));
    }

    IEnumerator LoadingScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            Debug.Log(progress);

            yield return null;
        }
    }
}
