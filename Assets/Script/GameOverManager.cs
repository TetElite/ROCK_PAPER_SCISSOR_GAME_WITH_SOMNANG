using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public Text resultText;
    public Button restartButton;
    public Button quitButton;

    void Start()
    {
        // Display the final result from GameManager
        if (resultText != null)
        {
            resultText.text = GameManager.finalResult;
        }

        // Setup button listeners
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitToMainMenu);
        }

        Debug.Log("GameOverManager started. Final result: " + GameManager.finalResult);
    }

    public void RestartGame()
    {
        Debug.Log("Restart button clicked");
        
        // Disable buttons to prevent multiple clicks
        if (restartButton != null) restartButton.interactable = false;
        if (quitButton != null) quitButton.interactable = false;
        
        // Load the game scene
        LoadScene("GameScene");
    }

    public void QuitToMainMenu()
    {
        Debug.Log("Quit button clicked");
        
        // Disable buttons to prevent multiple clicks
        if (restartButton != null) restartButton.interactable = false;
        if (quitButton != null) quitButton.interactable = false;
        
        // Load the main menu scene - CHANGED TO "MenuScene"
        LoadScene("MenuScene"); // Updated to match your scene name
    }

    private void LoadScene(string sceneName)
    {
        Debug.Log("Attempting to load scene: " + sceneName);
        
        // Check if scene exists in build settings
        if (IsSceneInBuildSettings(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found in build settings! Available scenes:");
            
            // List all available scenes for debugging
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string scene = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                Debug.Log($"Scene {i}: {scene}");
            }
            
            // Re-enable buttons since load failed
            if (restartButton != null) restartButton.interactable = true;
            if (quitButton != null) quitButton.interactable = true;
        }
    }

    private bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneInBuild = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            
            if (sceneInBuild == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    // Keyboard shortcuts
    void Update()
    {
        // Press R or Space to restart
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
        
        // Press M or Escape for main menu
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
        {
            QuitToMainMenu();
        }
    }
}