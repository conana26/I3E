/* Creator: Lim Xue Zhi Conan
   Date of Creation: 12/6/25
   Script: 	Tracks collectible sword progress */
using UnityEngine;
using TMPro;

public class SwordManager : MonoBehaviour
{
    public static SwordManager instance; // Singleton instance for global access

    public int totalSwords;              // Total number of swords to collect
    private int swordsCollected = 0;     // Current number of swords collected

    public TMP_Text collectedText;       // UI Text to show number of swords collected
    public TMP_Text remainingText;       // UI Text to show number of swords left

    void Awake()
    {
        // Implementing the Singleton pattern
        if (instance == null)
            instance = this;             // Set this as the instance if not set
        else
            Destroy(gameObject);         // Destroy duplicate manager if one already exists
    }

    void Start()
    {
        // Initialize the UI with current progress
        UpdateUI();
    }

    public void CollectSword()
    {
        swordsCollected++;               // Increment the collected sword count
        UpdateUI();                      // Refresh the UI to show new count

        // Check if all swords have been collected
        if (swordsCollected >= totalSwords)
        {
            Debug.Log("All Swords Collected.");
            // Optional: Show win UI or trigger event here
        }
    }

    public int GetCollectedCount()
    {
        // Returns the number of swords collected so far
        return swordsCollected;
    }

    void UpdateUI()
    {
        // Update UI text with current sword collection status
        collectedText.text = "Swords Collected: " + swordsCollected;
        remainingText.text = "Swords Left: " + (totalSwords - swordsCollected);
    }
}
