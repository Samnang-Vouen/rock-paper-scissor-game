using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] private string mainSceneName = "MainScenes";
    [SerializeField] private string startSceneName = "StartScenes";

    public void OnPlayAgain()
    {
        var state = GameState.Instance;
        if (state != null)
        {
            if (state.LastOutcome == GameState.Outcome.PlayerWin)
            {
                // Player won previously → reset scores to 0-0
                state.Reset();
            }
            else if (state.LastOutcome == GameState.Outcome.ComputerWin)
            {
                // Player lost previously → keep scores (typically 0-3)
                // No change required; GameHandler will read them.
            }
            state.LastOutcome = GameState.Outcome.None;
        }
        SceneManager.LoadScene(mainSceneName);
    }

    public void OnQuitGame()
    {
        // Return to StartScenes without forcing a score reset, per requirements.
        SceneManager.LoadScene(startSceneName);
    }
}
