using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public Slider sensitivitySlider; // Reference to the slider UI element
    //public CameraController cameraController; // Reference to the camera controller script

    private bool isPaused = false; // Tracks whether the game is paused

    void Start()
    {
        // Set the slider's default value to the camera's current sensitivity
        //sensitivitySlider.value = cameraController.mouseSensitivity;

        // Add a listener to update sensitivity when the slider value changes
        //sensitivitySlider.onValueChanged.AddListener(cameraController.SetMouseSensitivity);
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
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // change depending the name, for now i did mainMenu but the firstone like 0 could be better
    }
}
