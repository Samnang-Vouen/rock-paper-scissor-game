# rock-paper-scissor-game
This rock, paper, scissor game built in unity.

## Game Objective
- Reach 3 points before the computer using the tug-of-war scoring system.
- If the computer reaches 3 while you have 0, the game ends in `Game Over`.

## Game Logic
- Rock beats Scissors; Scissors beats Paper; Paper beats Rock.
- Tug-of-war scoring:
	- If the winner’s opponent has > 0 points: opponent loses 1 point.
	- Else: the winner gains 1 point.
- End conditions:
	- Player score = 3 → load `YouWin`.
	- Computer score = 3 and Player score = 0 → load `GameOver`.

## Number of Scenes
- `StartScenes`: Welcome screen with Play.
- `MainScenes`: Core gameplay (choices, scores, round results).
- `YouWin`: Shown when player wins.
- `GameOver`: Shown when computer wins under rule (computer=3, player=0).

## Scene Transitions
- Start → Main: Press `Play` in `StartScenes`.
- Main → YouWin: Player reaches 3 points.
- Main → GameOver: Computer reaches 3 while player is 0.
- YouWin → Main: `Play Again` resets scores to 0–0 and returns to `MainScenes`.
- GameOver → Main: `Play Again` keeps current losing scores (e.g., 0–3) and returns to `MainScenes`.
- YouWin/GameOver → Start: `Quit Game` loads `StartScenes`.

## How To Play
- Click Rock, Paper, or Scissors to play a round.
- Scores update each round per tug-of-war logic.

- How to Win:
	- Win enough rounds to reach 3 points total.
	- Each win reduces the opponent’s score first if they have > 0; otherwise it increases yours.

- How to Lose:
	- If the computer reaches 3 while your score is 0, you lose and see `GameOver`.

## Scoring Rules (Tug-of-War to 3)
- Winner increases their points only if the opponent is at 0.
- If the opponent has > 0, the win first reduces the opponent by 1 instead.
- First to reach 3 points wins the match.
- When the player reaches 3 → loads `YouWin` scene.
- When the computer reaches 3 while the player is 0 → loads `GameOver` scene.

Example:
- Player wins from 0–0 → Player: 1, Computer: 0
- Computer wins next → Player: 0 (reduced), Computer: 0
- Computer wins again → Player: 0, Computer: 1

## StartScenes → MainScenes Flow
- Add a GameObject with `SceneLoader` in `StartScenes`.
- Hook your Play button OnClick → `SceneLoader.LoadScene("MainScenes")`.
- Add a GameObject with `GameState` in `StartScenes` so the state persists across scenes.

## End Scenes Buttons Logic
- Attach `EndSceneController` (in `Assets/Scripts/EndSceneController.cs`) to a GameObject in both `YouWin` and `GameOver` scenes.
- Wire buttons:
	- `Play Again` → `EndSceneController.OnPlayAgain()`
		- If coming from `YouWin`: resets scores to `0-0` before loading `MainScenes`.
		- If coming from `GameOver`: keeps losing scores (typically `0-3`) so you can attempt a comeback.
	- `Quit Game` → `EndSceneController.OnQuitGame()` (loads `StartScenes`).

## Slide Presentation Link
- Slides: [Rock Paper Scissors Presentation](https://www.canva.com/design/DAG5zMIOwJg/RAAnsD0Wa8_R5TNDZNP1KA/edit?utm_content=DAG5zMIOwJg&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)