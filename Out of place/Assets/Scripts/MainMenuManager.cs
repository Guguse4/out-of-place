using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelMainMenu; 
    [SerializeField] private GameObject panelSettings; 

    [Header("Volume Control")]
    [SerializeField] private Slider volumeSlider; 
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        panelMainMenu.SetActive(true);
        panelSettings.SetActive(false);

        
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
        }

        
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
