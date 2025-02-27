using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject controlsPanel; // Assign in the Inspector
    public GameObject gameOverPanel;

    private void Start()
    {
        controlsPanel.SetActive(false); // Hide controls panel at start
        gameOverPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene1"); // Change to your game scene name
    }

    public void ToggleControls()
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf); // Show/hide controls panel
        if (controlsPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game (only works in build)
        Debug.Log("Game Quit"); // For testing in editor
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Scene1");
    }
}
