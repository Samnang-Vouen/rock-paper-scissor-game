using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
	public Image playerChoiceImage;
	public Image computerChoiceImage;
	public TMP_Text playerScoreText;
	public TMP_Text computerScoreText;
	public TMP_Text resultText;

	[Header("Scenes")]
	[Tooltip("Scene name to load when the player reaches 3 points")]
	public string youWinSceneName = "YouWin";
	[Tooltip("Scene name to load when the computer reaches 3 points and player is 0")]
	public string gameOverSceneName = "GameOver";

	public Sprite questionSprite;
	public Sprite rockSprite;
	public Sprite paperSprite;
	public Sprite scissorsSprite;

	private enum RpsChoice { None = 0, Rock = 1, Paper = 2, Scissors = 3 }
	private int playerScore;
	private int computerScore;

	private const int WinThreshold = 3;

	private void Start()
	{
		// Initialize scores from persistent GameState if available
		if (GameState.Instance != null)
		{
			playerScore = GameState.Instance.PlayerScore;
			computerScore = GameState.Instance.ComputerScore;
		}
		ResetRoundVisuals();
		UpdateScores();
	}

	public void SelectRock() { PlayerSelect((int)RpsChoice.Rock); }
	public void SelectPaper() { PlayerSelect((int)RpsChoice.Paper); }
	public void SelectScissors() { PlayerSelect((int)RpsChoice.Scissors); }

	public void PlayerSelect(int choiceIndex)
	{
		RpsChoice playerChoice = (RpsChoice)choiceIndex;
		if (playerChoice == RpsChoice.None) return;
		playerChoiceImage.sprite = GetSprite(playerChoice);

		RpsChoice computerChoice = (RpsChoice)Random.Range(1, 4);
		computerChoiceImage.sprite = GetSprite(computerChoice);

		EvaluateRound(playerChoice, computerChoice);
	}

	private void EvaluateRound(RpsChoice player, RpsChoice computer)
	{
		if (player == computer)
		{
			resultText.text = "Draw";
			return;
		}

		bool playerWins =
			(player == RpsChoice.Rock && computer == RpsChoice.Scissors) ||
			(player == RpsChoice.Paper && computer == RpsChoice.Rock) ||
			(player == RpsChoice.Scissors && computer == RpsChoice.Paper);

		// Tug-of-war scoring:
		// - If winner's opponent has > 0 points, first reduce opponent by 1.
		// - Otherwise, increment winner by 1.
		if (playerWins)
		{
			if (computerScore > 0)
			{
				computerScore -= 1;
			}
			else
			{
				playerScore += 1;
			}
			resultText.text = "You Win";
		}
		else
		{
			if (playerScore > 0)
			{
				playerScore -= 1;
			}
			else
			{
				computerScore += 1;
			}
			resultText.text = "Computer Wins";
		}

		UpdateScores();
		CheckForEndGame();
	}

	private void UpdateScores()
	{
		if (playerScoreText != null) playerScoreText.text = playerScore.ToString();
		if (computerScoreText != null) computerScoreText.text = computerScore.ToString();
	}

	private void CheckForEndGame()
	{
		// Player wins when reaching 3 points
		if (playerScore >= WinThreshold)
		{
			if (GameState.Instance != null)
			{
				GameState.Instance.PlayerScore = playerScore;
				GameState.Instance.ComputerScore = computerScore;
				GameState.Instance.LastOutcome = GameState.Outcome.PlayerWin;
			}
			TryLoadScene(youWinSceneName);
			return;
		}

		// Game Over appears only when computer has 3 and player has 0
		if (computerScore >= WinThreshold && playerScore == 0)
		{
			if (GameState.Instance != null)
			{
				GameState.Instance.PlayerScore = playerScore; // expected 0
				GameState.Instance.ComputerScore = computerScore; // expected 3
				GameState.Instance.LastOutcome = GameState.Outcome.ComputerWin;
			}
			TryLoadScene(gameOverSceneName);
		}
	}

	private void TryLoadScene(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName)) return;
		// Ensure the scene is included in Build Settings; otherwise, this will log an error at runtime.
		SceneManager.LoadScene(sceneName);
	}

	public void ResetRoundVisuals()
	{
		if (playerChoiceImage != null) playerChoiceImage.sprite = questionSprite;
		if (computerChoiceImage != null) computerChoiceImage.sprite = questionSprite;
		if (resultText != null) resultText.text = string.Empty;
	}

	private Sprite GetSprite(RpsChoice choice)
	{
		switch (choice)
		{
			case RpsChoice.Rock: return rockSprite;
			case RpsChoice.Paper: return paperSprite;
			case RpsChoice.Scissors: return scissorsSprite;
			default: return questionSprite;
		}
	}
}
