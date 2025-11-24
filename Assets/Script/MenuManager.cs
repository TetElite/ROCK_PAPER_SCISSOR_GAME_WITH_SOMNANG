using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button playButton;
    
    [Header("Scene Settings")]
    public string gameSceneName = "GameScene"; // Make sure this matches your game scene name
    
    [Header("Transition Settings")]
    public float sceneTransitionDelay = 0.5f;

    void Start()
    {
        // Assign the play button click event
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayGame);
        }
        else
        {
            Debug.LogError("Play button not assigned in MenuManager!");
        }
    }

    public void PlayGame()
    {
        Debug.Log("Loading game scene: " + gameSceneName);
        
        // Optional: Add button click effect
        StartCoroutine(ButtonClickEffect());
        
        // Disable button to prevent multiple clicks
        playButton.interactable = false;
        
        // Load scene after delay
        Invoke("LoadGameScene", sceneTransitionDelay);
    }

    private void LoadGameScene()
    {
        if (Application.CanStreamedLevelBeLoaded(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogError("Scene not found: " + gameSceneName);
            playButton.interactable = true; // Re-enable if failed
        }
    }

    private IEnumerator ButtonClickEffect()
    {
        // Simple button press animation
        Vector3 originalScale = playButton.transform.localScale;
        playButton.transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
        playButton.transform.localScale = originalScale;
    }
}