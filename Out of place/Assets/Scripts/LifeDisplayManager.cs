using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private Transform lifeContainer;
    [SerializeField] private TextMeshProUGUI bottomLeftTextDisplay; // Text display for bottom-left messages
    [SerializeField] private List<string> bottomLeftMessages; // List of messages to display
    [SerializeField] private float messageDisplayInterval = 3f; // Time in seconds between messages
    [SerializeField] private float clearMessageDelay = 5f;
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

    private int currentMessageIndex = 0;
    private float messageTimer;
    private bool allMessagesDisplayed = false;
    private bool clearMessageTriggered = false;
    private float clearMessageTimer = 0f;

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

        // Initialize the text display
        //bottomLeftTextDisplay.text = ""; // Start with an empty display
        if (bottomLeftMessages.Count > 0)
        {
            bottomLeftTextDisplay.text = bottomLeftMessages[0] + "\n";
            currentMessageIndex = 1; // Start from the second message for the timer logic
        }
    }

    void Update()
    {
        // Update life display dynamically
        UpdateLifeDisplay();
        HandleTextDisplay();
        HandleMessageClear();
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

    private void HandleTextDisplay()
    {
        if (allMessagesDisplayed || bottomLeftMessages.Count == 0) return;

        // Update the timer
        messageTimer += Time.deltaTime;

        if (messageTimer >= messageDisplayInterval)
        {
            // Display the next message if available
            if (currentMessageIndex < bottomLeftMessages.Count)
            {
                bottomLeftTextDisplay.text += bottomLeftMessages[currentMessageIndex] + "\n"; // Append the new message
                currentMessageIndex++;
            }
            else
            {
                // All messages have been displayed
                allMessagesDisplayed = true;
                clearMessageTriggered = true; // Start the clear message timer
            }

            messageTimer = 0f; // Reset the timer
        }

    }
    private void HandleMessageClear()
    {
        if (!clearMessageTriggered) return;

        // Update the clear message timer
        clearMessageTimer += Time.deltaTime;

        if (clearMessageTimer >= clearMessageDelay)
        {
            // Clear all messages
            bottomLeftTextDisplay.text = "";
            clearMessageTriggered = false; // Reset the clear trigger
        }
    }
}
