using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum Outcome { None, PlayerWin, ComputerWin }

    public static GameState Instance { get; private set; }

    public int PlayerScore { get; set; }
    public int ComputerScore { get; set; }
    public Outcome LastOutcome { get; set; } = Outcome.None;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Reset()
    {
        PlayerScore = 0;
        ComputerScore = 0;
    }
}
