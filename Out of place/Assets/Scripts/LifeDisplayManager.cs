using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager; // Reference to the PlayerManager script
    [SerializeField] private GameObject lifePrefab;       // Prefab for the life rectangle
    [SerializeField] private Transform lifeContainer;     // UI container for the life rectangles

    private List<GameObject> lifeIcons = new List<GameObject>(); // List to store active life rectangles

    void Start()
    {
        if (playerManager == null || lifePrefab == null || lifeContainer == null)
        {
            Debug.LogError("References are missing in LifeDisplayManager!");
            return;
        }

        UpdateLifeDisplay(); // Initialize the life display
    }

    void Update()
    {
        UpdateLifeDisplay(); // Update the life display continuously
    }

    private void UpdateLifeDisplay()
    {
        // Ensure the number of life rectangles matches the player's lives
        int currentLives = playerManager.numberOfHP;

        // Remove excess life rectangles if lives have decreased
        while (lifeIcons.Count > currentLives)
        {
            Destroy(lifeIcons[lifeIcons.Count - 1]); // Destroy the last life icon
            lifeIcons.RemoveAt(lifeIcons.Count - 1);
        }

        // Add new life rectangles if lives have increased
        while (lifeIcons.Count < currentLives)
        {
            GameObject newLife = Instantiate(lifePrefab, lifeContainer); // Create a new life rectangle
            lifeIcons.Add(newLife); // Add it to the list
        }
    }
}
