using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float delay = 1f; // Temps d'attente avant le changement de scène

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in contact");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player in contact");
            Invoke("ChangeScene", delay);
        }
    }

    private void ChangeScene()
    {
        // ici s'occuper du son du mimic

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
