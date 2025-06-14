/* Creator: Lim Xue Zhi Conan
   Date of Creation: 13/6/25
   Script: Handles keycard collection logic with UI updates, sound, shrinking animation, and temp message */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeycardCollect : MonoBehaviour
{
    public GameObject collectPrompt;       // UI: "Press E to collect keycard"
    public GameObject keycardUIImage;      // UI icon that appears when keycard is collected
    public GameObject tempMessage;         // UI message: "You can now open a door"
    public AudioSource collectSound;       // Sound when keycard is picked up
    public float messageDuration = 3f;     // How long to show the temp message

    private bool isPlayerNear = false;     // Is the player in range to collect
    private bool isCollected = false;      // Has the keycard been picked up

    void Start()
    {
        // Hide all UI elements initially
        if (collectPrompt) collectPrompt.SetActive(false);
        if (tempMessage) tempMessage.SetActive(false);
        if (keycardUIImage) keycardUIImage.SetActive(false);
    }

    void Update()
    {
        // If player is nearby and presses E, collect the keycard
        if (isPlayerNear && !isCollected && Input.GetKeyDown(KeyCode.E))
        {
            CollectKeycard();
        }
    }

    void CollectKeycard()
    {
        isCollected = true;

        // Hide prompt
        if (collectPrompt) collectPrompt.SetActive(false);

        // Animate and disable the keycard object
        StartCoroutine(ShrinkAndDisappear());

        // Play pickup sound
        if (collectSound) collectSound.Play();

        // Show keycard icon on screen
        if (keycardUIImage) keycardUIImage.SetActive(true);

        // Show "You can now open a door" message briefly
        if (tempMessage) StartCoroutine(ShowTempMessage());

        // Update static flag so other scripts know we have it
        KeycardManager.hasKeycard = true;
    }

    IEnumerator ShrinkAndDisappear()
    {
        Vector3 originalScale = transform.localScale;
        float shrinkDuration = 0.5f;
        float elapsed = 0f;

        // Smooth shrink animation over 0.5 seconds
        while (elapsed < shrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsed / shrinkDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false); // Hide the keycard object
    }

    IEnumerator ShowTempMessage()
    {
        tempMessage.SetActive(true);                   // Show message
        yield return new WaitForSeconds(messageDuration); // Wait for duration
        tempMessage.SetActive(false);                  // Hide message
    }

    private void OnTriggerEnter(Collider other)
    {
        // Show collect prompt if player enters range
        if (other.CompareTag("Player") && !isCollected)
        {
            isPlayerNear = true;
            if (collectPrompt) collectPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide collect prompt if player leaves range
        if (other.CompareTag("Player") && !isCollected)
        {
            isPlayerNear = false;
            if (collectPrompt) collectPrompt.SetActive(false);
        }
    }
}
