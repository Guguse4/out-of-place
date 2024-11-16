using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject lostMenuPanel; 
    [SerializeField] private PlayerManager playerManager; 

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
            Time.timeScale = 0f; 
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true; 
        }
    }

    
    public void LoadMainMenu()
    {
        // Load the main menu scene
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}
