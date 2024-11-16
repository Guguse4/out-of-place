using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio; // Required for AudioMixer

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;       // Reference to the pause menu UI
    public Slider sensitivitySlider;    // Reference to the slider for sensitivity
    public Slider volumeSlider;         // Reference to the slider for volume
    public PlayerCam playerCam;         // Reference to the PlayerCam script
    public AudioMixer audioMixer;       // Reference to the AudioMixer

    private bool isPaused = false;      // Tracks whether the game is paused
    public bool IsPaused => isPaused;
    void Start()
    {
        // Initialize sensitivity slider
        sensitivitySlider.value = playerCam.sensX;
        //Debug.Log(playerCam.sensX);
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);

        // Initialize volume slider
        volumeSlider.value = 0.75f; // Default value (change as needed)
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
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
        //Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void UpdateSensitivity(float newSensitivity)
    {
        playerCam.sensX = newSensitivity;
        playerCam.sensY = newSensitivity;
    }

    private void UpdateVolume(float newVolume)
    {
        float volumeInDecibels = Mathf.Log10(Mathf.Clamp(newVolume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", volumeInDecibels);
    }
}
