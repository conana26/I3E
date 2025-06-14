using UnityEngine;
using TMPro;

public class SwordManager : MonoBehaviour
{
    public static SwordManager instance;

    public int totalSwords;
    private int swordsCollected = 0;

    public TMP_Text collectedText;
    public TMP_Text remainingText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void CollectSword()
    {
        swordsCollected++;
        UpdateUI();

        if (swordsCollected >= totalSwords)
        {
            Debug.Log("All Swords Collected.");
            // Optional: show win UI
        }
    }

    public int GetCollectedCount()
    {
       return swordsCollected;
    }

    void UpdateUI()
    {
        collectedText.text = "Swords Collected: " + swordsCollected;
        remainingText.text = "Swords Left: " + (totalSwords - swordsCollected);
    }
}

