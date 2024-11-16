using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float delay = 1f; // Temps d'attente avant le changement de sc�ne
    public AudioSource masterAudioSource; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            SyncMimicAudio();

            // D�clenche le changement de sc�ne apr�s un d�lai
            Invoke("ChangeScene", delay);
        }
    }

    private void SyncMimicAudio()
    {
        // Trouver tous les objets avec le tag "Mimic"
        GameObject[] mimicObjects = GameObject.FindGameObjectsWithTag("Mimic");

        foreach (GameObject mimic in mimicObjects)
        {
            // V�rifier si l'objet a un AudioSource
            AudioSource mimicAudio = mimic.GetComponent<AudioSource>();
            if (mimicAudio != null && masterAudioSource != null)
            {
                // Copier les param�tres de volume et pitch depuis le master
                mimicAudio.volume = masterAudioSource.volume;
                mimicAudio.pitch = masterAudioSource.pitch;
            }
        }
    }

    private void ChangeScene()
    {
        // Charger la sc�ne suivante dans le Build Settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
