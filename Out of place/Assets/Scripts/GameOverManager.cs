using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject lostMenuPanel; // Reference to the Lost Menu Panel
    [SerializeField] private PlayerManager playerManager; // Reference to the PlayerManager script

    void Update()
    {
        // Check if the player has lost the game
        if (playerManager != null && playerManager.GetIsGameOver())
        {
            ShowGameOver();
        }
    }

    private void ShowGameOver()
    {
        // Show the Game Over panel and pause the game
        if (lostMenuPanel != null && !lostMenuPanel.activeSelf)
        {
            lostMenuPanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Show the cursor
        }
    }

    
    public void LoadMainMenu()
    {
        // Load the main menu scene
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu"); 
    }
}
