using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelMainMenu; // Reference to the Main Menu Panel
    [SerializeField] private GameObject panelSettings; // Reference to the Settings Panel

    [Header("Volume Control")]
    [SerializeField] private Slider volumeSlider; // Reference to the Volume Slider
    [SerializeField] private AudioMixer audioMixer; // Reference to the Audio Mixer

    private void Start()
    {
        // Ensure the Main Menu Panel is active and the Settings Panel is inactive
        panelMainMenu.SetActive(true);
        panelSettings.SetActive(false);

        // Initialize the volume slider
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f); // Default to 75% volume
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
        }

        // Add a listener to the volume slider to update the volume
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void StartGame()
    {
        // Load the next scene in the build order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void OpenSettings()
    {
        panelMainMenu.SetActive(false);
        panelSettings.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        panelSettings.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    private void SetVolume(float volume)
    {
        if (audioMixer != null)
        {
            float volumeInDecibels = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
            audioMixer.SetFloat("MasterVolume", volumeInDecibels);
        }

        // Save the volume setting
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
