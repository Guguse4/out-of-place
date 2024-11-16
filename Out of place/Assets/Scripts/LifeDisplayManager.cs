using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private Transform lifeContainer;
    [SerializeField] public bool IsLevel3 = false;

    private List<GameObject> lifeIcons = new List<GameObject>();

    void Start()
    {
        if (playerManager == null || lifePrefab == null || lifeContainer == null)
        {
            Debug.LogError("References are missing in LifeDisplayManager!");
            return;
        }


        // Initialize the life display
        if (IsLevel3)
            UpdateLifeDisplayLevel3();
        else
            UpdateLifeDisplayNormal();
    }

    void Update()
    {
        if (IsLevel3)
            UpdateLifeDisplayLevel3();
        else
            UpdateLifeDisplayNormal();
    }

    private void UpdateLifeDisplayNormal()
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
            GameObject newLife = Instantiate(lifePrefab, lifeContainer);
            lifeIcons.Add(newLife);
        }
    }
    private void UpdateLifeDisplayLevel3()
    {
        int currentLives = playerManager.numberOfHP + 1; // Add one extra life in Level 3

        // Check if the player dies at 1 life
        if (playerManager.numberOfHP == 0)// No sense to have to put 0
        {
            HandlePlayerDeath();
            return;
        }

        while (lifeIcons.Count > currentLives)
        {
            Destroy(lifeIcons[lifeIcons.Count - 1]); // Destroy the last life icon
            lifeIcons.RemoveAt(lifeIcons.Count - 1);
        }

        // Add new life rectangles if lives have increased
        while (lifeIcons.Count < currentLives)
        {
            GameObject newLife = Instantiate(lifePrefab, lifeContainer);
            lifeIcons.Add(newLife);
        }
    }

    private void HandlePlayerDeath()
    {
        UpdateLifeDisplayNormal();
        playerManager.numberOfHP = 0;

    }

}
