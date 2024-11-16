using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float delay = 1f; // Temps d'attente avant le changement de scène
    public AudioSource masterAudioSource; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            SyncMimicAudio();

            // Déclenche le changement de scène après un délai
            Invoke("ChangeScene", delay);
        }
    }

    private void SyncMimicAudio()
    {
        // Trouver tous les objets avec le tag "Mimic"
        GameObject[] mimicObjects = GameObject.FindGameObjectsWithTag("Mimic");

        foreach (GameObject mimic in mimicObjects)
        {
            // Vérifier si l'objet a un AudioSource
            AudioSource mimicAudio = mimic.GetComponent<AudioSource>();
            if (mimicAudio != null && masterAudioSource != null)
            {
                // Copier les paramètres de volume et pitch depuis le master
                mimicAudio.volume = masterAudioSource.volume;
                mimicAudio.pitch = masterAudioSource.pitch;
            }
        }
    }

    private void ChangeScene()
    {
        // Charger la scène suivante dans le Build Settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
