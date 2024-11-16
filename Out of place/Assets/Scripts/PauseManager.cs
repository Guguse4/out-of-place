using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio; 

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;       
    public Slider sensitivitySlider;    
    public Slider volumeSlider;         
    public PlayerCam playerCam;         
    public AudioMixer audioMixer;
    public LightDoor[] lightDoors;
    public PlayerManager playerManager;


    [SerializeField] private LifeDisplayManager lifeDisplayManager;


    private bool isPaused = false;      // Tracks whether the game is paused
    public bool IsPaused => isPaused;
    void Start()
    {
        // Initialize sensitivity slider
        sensitivitySlider.value = playerCam.sensX;
        //Debug.Log(playerCam.sensX);
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);

        // Initialize volume slider
        volumeSlider.value = 0.75f; 
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
    public void KillMimic(GameObject mimic)
    {
        MimicManager mimicManager = mimic.GetComponent<MimicManager>();

        for (int i = 0; i < lightDoors.Length; i++)
        {
            lightDoors[i].SetLightOn(mimicManager.GetIdMimic());
        }
        

        // Destroy the mimic object
        Destroy(mimic); //faire autre chose enlever la vie  ici changer le booleen de IsLevel3 en false
        // Instead of destroying the mimic, adjust the game state
        if (lifeDisplayManager != null)
        {
            lifeDisplayManager.IsLevel3 = false; // Disable Level 3 behavior
        }

        playerManager.numberOfMimicFound++;
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
