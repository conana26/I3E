/* Creator: Lim Xue Zhi Conan
   Date of Creation: 14/6/25
   Script: 	Grants crystal if all swords found */
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CrystalClaim : MonoBehaviour
{
    public GameObject uiPrompt;         // "Press E to claim the crystal" prompt UI
    public GameObject winUI;            // Victory UI to display after claiming the crystal
    public GameObject crystalImage;     // Image or animation of the crystal in the center of the screen
    public GameObject notEnoughUI;      // UI shown when player hasn’t collected all swords

    public AudioSource winSound;        // Sound played when crystal is successfully claimed
    public AudioSource sadSound;        // Sound played when player doesn’t have enough swords

    private bool isPlayerNear = false;      // Tracks if player is within claim zone
    private bool hasBeenClaimed = false;    // Prevents multiple claims

    void Update()
    {
        // Check if player is nearby, crystal hasn't been claimed, and player presses E
        if (isPlayerNear && !hasBeenClaimed && Input.GetKeyDown(KeyCode.E))
        {
            // Get collected and total sword counts from SwordManager
            int collected = SwordManager.instance.GetCollectedCount();
            int total = SwordManager.instance.totalSwords;

            // If player has all swords, claim the crystal
            if (collected >= total)
            {
                StartCoroutine(HandleCrystalClaim());
            }
            else
            {
                // Show "not enough swords" UI and play sad sound
                ShowNotEnoughUI();
            }
        }
    }

    IEnumerator HandleCrystalClaim()
    {
        hasBeenClaimed = true; // Mark as claimed to prevent duplicate activation

        // Hide prompts and "not enough" UI
        if (uiPrompt) uiPrompt.SetActive(false);
        if (notEnoughUI) notEnoughUI.SetActive(false);

        // Show win UI and crystal image
        if (winUI) winUI.SetActive(true);
        if (crystalImage) crystalImage.SetActive(true);

        float waitTime = 5f; // Minimum time to show win screen

        // Play win sound and use its duration if longer than default wait time
        if (winSound)
        {
            winSound.Play();
            waitTime = Mathf.Max(winSound.clip.length, waitTime);
        }

        // Animate the crystal shrinking over time
        Vector3 originalScale = transform.localScale;
        float shrinkDuration = 0.5f;
        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsed / shrinkDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure it ends fully shrunk and disable the crystal GameObject
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);

        // Wait before hiding the win UI
        yield return new WaitForSeconds(waitTime);

        if (winUI) winUI.SetActive(false);
        if (crystalImage) crystalImage.SetActive(false);
    }

    void ShowNotEnoughUI()
    {
        // Hide the claim prompt and show the "not enough swords" message
        if (uiPrompt) uiPrompt.SetActive(false);
        if (notEnoughUI) notEnoughUI.SetActive(true);

        // Play sad sound if assigned
        if (sadSound) sadSound.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        // When player enters the claim zone and crystal hasn’t been claimed
        if (other.CompareTag("Player") && !hasBeenClaimed)
        {
            isPlayerNear = true;
            if (uiPrompt) uiPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // When player exits the claim zone, hide the claim prompt
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (uiPrompt) uiPrompt.SetActive(false);
        }
    }
}
