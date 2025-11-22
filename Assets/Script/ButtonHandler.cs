using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameManager gameManager;

    public void OnRockButton()
    {
        gameManager.OnPlayerChoice(0);  // 0 = Rock
    }

    public void OnPaperButton()
    {
        gameManager.OnPlayerChoice(1);  // 1 = Paper
    }

    public void OnScissorsButton()
    {
        gameManager.OnPlayerChoice(2);  // 2 = Scissors
    }
}