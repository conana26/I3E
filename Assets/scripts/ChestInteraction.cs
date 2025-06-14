/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 13/6/25
   Script: Handles interaction with chests that spawn sword, keycard, or cause death */
using UnityEngine;
using System.Collections;

public class ChestInteraction : MonoBehaviour
{
    public enum ChestType { Death, Sword, Keycard } // Different outcomes from the chest
    public ChestType chestOutcome;

    public GameObject swordPrefab;     // Sword to spawn
    public GameObject keycardPrefab;   // Keycard to spawn
    public Transform spawnPoint;       // Where the item spawns

    public GameObject uiPrompt;        // "Press E" prompt
    public Animator animator;          // Animator for chest lid
    public AudioSource chestOpenSound; // Sound when opening
    public AudioSource deathSound;     // Sound for death chest

    private bool isPlayerNear = false; // If player is in range
    private bool isOpened = false;     // Prevents re-opening
    private GameObject player;         
    private PlayerHealth playerHealth; // Reference to player's health script

    private Rigidbody rb;

    void Start()
    {
        // Disable physics so chest stays in place
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void Update()
    {
        // If player is near and presses E, open the chest
        if (isPlayerNear && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        if (uiPrompt) uiPrompt.SetActive(false);    // Hide UI prompt
        if (chestOpenSound) chestOpenSound.Play();  // Play opening sound
        if (animator) animator.SetTrigger("Open");  // Trigger open animation

        StartCoroutine(HandleChestAfterOpen());     // Handle spawn & effects
    }

    IEnumerator HandleChestAfterOpen()
    {
        yield return new WaitForSeconds(1.5f); // Wait for animation

        // Spawn item based on chest type
        if (chestOutcome == ChestType.Sword && swordPrefab && spawnPoint)
            Instantiate(swordPrefab, spawnPoint.position, spawnPoint.rotation);
        else if (chestOutcome == ChestType.Keycard && keycardPrefab && spawnPoint)
            Instantiate(keycardPrefab, spawnPoint.position, spawnPoint.rotation);

        // Smoothly shrink chest
        Vector3 originalScale = transform.localScale;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;

        // Kill player if chest is deadly
        if (chestOutcome == ChestType.Death)
        {
            if (deathSound) deathSound.Play();
            if (playerHealth != null)
                playerHealth.TakeDamage(playerHealth.maxHealth);
        }

        gameObject.SetActive(false); // Hide the chest
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player gets close, show UI and track health script
        if (other.CompareTag("Player") && !isOpened)
        {
            isPlayerNear = true;
            if (uiPrompt) uiPrompt.SetActive(true);

            player = other.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide UI prompt when player walks away
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (uiPrompt) uiPrompt.SetActive(false);
        }
    }
}
