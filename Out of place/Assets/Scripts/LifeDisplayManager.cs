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

    private const int MaxLives = 4; // Maximum number of lives
    private readonly Vector2[] IconPositions =
    {
        new Vector2(-300, 0), // Position for the first life icon
        new Vector2(-100, 0),  // Position for the second life icon
        new Vector2(100, 0),   // Position for the third life icon
        new Vector2(300, 0)    // Position for the fourth life icon
    };

    void Start()
    {
        if (playerManager == null || lifePrefab == null || lifeContainer == null)
        {
            Debug.LogError("References are missing in LifeDisplayManager!");
            return;
        }

        InitializeLifeIcons();

        // Initial update to set life icons correctly
        UpdateLifeDisplay();
    }

    void Update()
    {
        // Update life display dynamically
        UpdateLifeDisplay();
    }

    private void InitializeLifeIcons()
    {
        // Clear existing icons (if any)
        foreach (var icon in lifeIcons)
        {
            Destroy(icon);
        }
        lifeIcons.Clear();

        // Create the maximum number of life icons and position them
        for (int i = 0; i < MaxLives; i++)
        {
            GameObject newLife = Instantiate(lifePrefab, lifeContainer);
            RectTransform rectTransform = newLife.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = IconPositions[i]; // Set position
            }
            newLife.SetActive(false); // Initially, set all icons to inactive
            lifeIcons.Add(newLife);
        }
    }

    private void UpdateLifeDisplay()
    {
        // Determine the current number of lives
        int currentLives = playerManager.numberOfHP;

        if (IsLevel3)
        {
            currentLives += 1; // Add an extra life for Level 3
        }

        // Clamp currentLives to match the number of icons
        currentLives = Mathf.Clamp(currentLives, 0, MaxLives);

        // Update icon visibility
        for (int i = 0; i < lifeIcons.Count; i++)
        {
            lifeIcons[i].SetActive(i < currentLives);
        }

        // Handle player death logic if applicable
        if (currentLives == 0 && IsLevel3)
        {
            HandlePlayerDeath();
        }
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Player has died!");
        playerManager.numberOfHP = 0;
        UpdateLifeDisplay(); // Update display to reflect zero lives
    }
}
