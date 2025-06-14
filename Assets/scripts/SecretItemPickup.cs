/* 
 Creator: Lim Xue Zhi Conan
 Date Of Creation: 13/6/25
 Script: Collectable secret item (Axe) that shows permanent UI when picked 
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecretItemPickup : MonoBehaviour
{
    public GameObject interactionUI;      // UI prompt shown when player is nearby ("Press E to collect")
    public GameObject secretItemUI;       // Permanent UI icon that stays visible after collecting the axe
    public GameObject axeModel;           // The visible 3D model of the axe (will shrink and disappear)
    public AudioSource pickupSound;       // Sound that plays when item is collected

    private bool isPlayerNearby = false;  // Tracks if the player is near the item
    private bool isCollected = false;     // Tracks if the item has already been picked up

    void Update()
    {
        // Listen for 'E' key press when player is near and item hasn't been collected yet
        if (isPlayerNearby && !isCollected && Input.GetKeyDown(KeyCode.E))
        {
            CollectAxe();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If already collected, don't respond to triggers
        if (isCollected) return;

        // If the player enters the trigger zone, show interaction prompt
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionUI) interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger zone, hide interaction prompt
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionUI) interactionUI.SetActive(false);
        }
    }

    void CollectAxe()
    {
        isCollected = true;  // Prevent future collection

        // Hide interaction prompt and show permanent collected UI
        if (interactionUI) interactionUI.SetActive(false);
        if (secretItemUI) secretItemUI.SetActive(true);

        // Play pickup sound
        if (pickupSound)
        {
            pickupSound.Stop();  // Ensure sound restarts from beginning
            pickupSound.Play();
        }

        // Start shrinking and disabling the axe model
        if (axeModel != null)
        {
            StartCoroutine(ShrinkAndDisableModel());
        }
    }

    IEnumerator ShrinkAndDisableModel()
    {
        float duration = 0.5f;  // Shrinking duration in seconds
        float time = 0f;
        Vector3 originalScale = axeModel.transform.localScale;

        // Gradually shrink the axe model over time
        while (time < duration)
        {
            axeModel.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // Fully hide and deactivate the axe model
        axeModel.transform.localScale = Vector3.zero;
        axeModel.SetActive(false);
    }
}
