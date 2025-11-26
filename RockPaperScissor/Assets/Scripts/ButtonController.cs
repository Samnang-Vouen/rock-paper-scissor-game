using UnityEngine;

public class ButtonController : MonoBehaviour
{
	public GameHandler gameHandler;
	[Range(1,3)] public int choiceIndex; // 1=Rock,2=Paper,3=Scissors

	public void OnClick()
	{
		if (gameHandler != null)
		{
			gameHandler.PlayerSelect(choiceIndex);
		}
	}
}
