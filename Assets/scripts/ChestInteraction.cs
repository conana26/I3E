using UnityEngine;
using System.Collections;

public class ChestInteraction : MonoBehaviour
{
    public enum ChestType { Death, Sword, Keycard }
    public ChestType chestOutcome;

    public GameObject swordPrefab;
    public GameObject keycardPrefab;
    public Transform spawnPoint;

    public GameObject uiPrompt;
    public Animator animator;
    public AudioSource chestOpenSound;
    public AudioSource deathSound;

    private bool isPlayerNear = false;
    private bool isOpened = false;
    private GameObject player;
    private PlayerHealth playerHealth;

    private Rigidbody rb;

    void Start()
    {
        // Prevent chest from being moved by physics
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void Update()
    {
        if (isPlayerNear && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        if (uiPrompt) uiPrompt.SetActive(false);

        if (chestOpenSound) chestOpenSound.Play();

        if (animator) animator.SetTrigger("Open");

        StartCoroutine(HandleChestAfterOpen());
    }

    IEnumerator HandleChestAfterOpen()
    {
        yield return new WaitForSeconds(1.5f); // Let animation play

        if (chestOutcome == ChestType.Sword && swordPrefab && spawnPoint)
            Instantiate(swordPrefab, spawnPoint.position, spawnPoint.rotation);
        else if (chestOutcome == ChestType.Keycard && keycardPrefab && spawnPoint)
            Instantiate(keycardPrefab, spawnPoint.position, spawnPoint.rotation);

        // Shrink chest smoothly
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

        // Death chest kills player
        if (chestOutcome == ChestType.Death)
        {
            if (deathSound) deathSound.Play();
            if (playerHealth != null)
                playerHealth.TakeDamage(playerHealth.maxHealth);
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
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
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (uiPrompt) uiPrompt.SetActive(false);
        }
    }
}
