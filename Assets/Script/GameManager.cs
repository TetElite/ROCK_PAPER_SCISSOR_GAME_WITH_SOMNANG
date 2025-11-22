using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public Image playerChoiceImage;      // Left ? image (My_Choice)
    public Image computerChoiceImage;    // Right ? image (Computer_Choice)
    public Text resultText;              // "Select the button below" text
    public Text playerScoreText;         // You_Text score display
    public Text computerScoreText;       // Computer_Text score display

    [Header("Choice Sprites")]
    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;
    public Sprite questionMarkSprite;    // The ? image

    [Header("Game Settings")]
    public int scoreToWin = 3;  // Need to reach +3 or -3 to win

    private int currentScore = 0;  // Starts at 0, goes up or down
    private bool gameOver = false;

    void Start()
    {
        UpdateScoreDisplay();
        resultText.text = "Select the button below";
    }

    public void OnPlayerChoice(int choice)
    {
        if (gameOver) return;

        // Player's choice: 0 = Rock, 1 = Paper, 2 = Scissors
        int computerChoice = Random.Range(0, 3);

        // Display choices
        DisplayChoice(playerChoiceImage, choice);
        DisplayChoice(computerChoiceImage, computerChoice);

        // Update result text temporarily
        resultText.text = "...";

        // Calculate winner after a short delay
        StartCoroutine(DetermineWinner(choice, computerChoice));
    }

    IEnumerator DetermineWinner(int playerChoice, int computerChoice)
    {
        yield return new WaitForSeconds(1f);

        // Game logic
        if (playerChoice == computerChoice)
        {
            resultText.text = "It's a Draw!";
            // Score stays the same on a draw
        }
        else if ((playerChoice == 0 && computerChoice == 2) ||  // Rock beats Scissors
                 (playerChoice == 1 && computerChoice == 0) ||  // Paper beats Rock
                 (playerChoice == 2 && computerChoice == 1))    // Scissors beats Paper
        {
            currentScore++;  // Player wins, score goes UP
            resultText.text = "You Win This Round!";
        }
        else
        {
            currentScore--;  // Computer wins, score goes DOWN
            resultText.text = "Computer Wins This Round!";
        }

        UpdateScoreDisplay();

        // Check for game over
        yield return new WaitForSeconds(1.5f);
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (currentScore >= scoreToWin)
        {
            resultText.text = "You Win!";
            gameOver = true;
        }
        else if (currentScore <= -scoreToWin)
        {
            resultText.text = "Computer Wins!";
            gameOver = true;
        }
        else
        {
            // Reset for next round
            resultText.text = "Select the button below";
            playerChoiceImage.sprite = questionMarkSprite;
            computerChoiceImage.sprite = questionMarkSprite;
        }
    }

    void DisplayChoice(Image targetImage, int choice)
    {
        switch (choice)
        {
            case 0:
                targetImage.sprite = rockSprite;
                break;
            case 1:
                targetImage.sprite = paperSprite;
                break;
            case 2:
                targetImage.sprite = scissorsSprite;
                break;
        }
    }

    void UpdateScoreDisplay()
    {
        // Calculate individual display scores
        int playerDisplayScore = Mathf.Max(0, currentScore);
        int computerDisplayScore = Mathf.Max(0, -currentScore);

        playerScoreText.text = playerDisplayScore.ToString();
        computerScoreText.text = computerDisplayScore.ToString();
    }

    public void ResetGame()
    {
        currentScore = 0;
        gameOver = false;
        playerChoiceImage.sprite = questionMarkSprite;
        computerChoiceImage.sprite = questionMarkSprite;
        UpdateScoreDisplay();
        resultText.text = "Select the button below";
    }
}