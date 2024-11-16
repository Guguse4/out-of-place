using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;       // Reference to the pause menu UI
    public Slider sensitivitySlider;    // Reference to the slider UI element
    public PlayerCam playerCam;         // Reference to the PlayerCam script

    private bool isPaused = false;      // Tracks whether the game is paused

    void Start()
    {
        sensitivitySlider.value = playerCam.sensX;
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log(Cursor.lockState);
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log(Cursor.lockState);
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // to change 
    }

    private void UpdateSensitivity(float newSensitivity)
    {
        playerCam.sensX = newSensitivity;
        playerCam.sensY = newSensitivity;
    }
}
