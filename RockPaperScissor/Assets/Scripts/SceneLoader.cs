using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    void Awake()
    {
        Debug.Log($"SceneLoader: Awake on '{gameObject.name}' (active={gameObject.activeInHierarchy})");
    }

    public void LoadMainScenes()
    {
        LoadScene("MainScenes");
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("SceneLoader: sceneName is empty");
            return;
        }

        Debug.Log($"SceneLoader: attempting to load '{sceneName}'");
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"SceneLoader: Scene '{sceneName}' is not in Build Settings or cannot be loaded.");
            return;
        }

        SceneManager.LoadScene(sceneName);
        // Confirm scene change next frame
        StartCoroutine(LogActiveSceneNextFrame());
    }

    private IEnumerator LogActiveSceneNextFrame()
    {
        yield return null;
        var active = SceneManager.GetActiveScene();
        Debug.Log($"SceneLoader: Active scene is now '{active.name}' (index {active.buildIndex})");
    }

    public void ReloadCurrent()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void TestLog(){
        Debug.Log("Button onclick fired");
    }
}
