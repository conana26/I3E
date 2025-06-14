/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 12/6/25
   Script: Player health system with damage flash, death, and respawn handling */

using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public TextMeshProUGUI healthText;

    public AudioSource damageAudio;
    public AudioSource deathAudio;
    public Image damageFlashImage;
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); // Red flash
    public float flashSpeed = 5f;

    public GameObject deathScreen;
    public float respawnDelay = 3f;

    public Camera playerCamera;
    public float zoomFOV = 40f;           // Zoom in when dying
    public float zoomDuration = 0.5f;

    public Transform respawnPoint;        // 🧍‍♂️ Location to respawn

    private bool isDead = false;
    private bool damaged = false;
    private float defaultFOV;

    private StarterAssetsInputs inputScript;
    private FirstPersonController movementScript;
    private CharacterController characterController;

    void Start()
    {
        health = maxHealth;
        UpdateHealthUI();

        if (damageFlashImage != null)
            damageFlashImage.color = Color.clear;

        if (deathScreen != null)
            deathScreen.SetActive(false);

        // Cache components
        inputScript = GetComponent<StarterAssetsInputs>();
        movementScript = GetComponent<FirstPersonController>();
        characterController = GetComponent<CharacterController>();

        if (playerCamera != null)
            defaultFOV = playerCamera.fieldOfView;
    }

    void Update()
    {
        // If damaged this frame, show flash
        if (damaged)
        {
            damageFlashImage.color = flashColor;
        }
        else if (damageFlashImage != null)
        {
            // Smoothly fade flash
            damageFlashImage.color = Color.Lerp(damageFlashImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        if (health < 0) health = 0;

        damaged = true;

        if (damageAudio != null)
            damageAudio.Play();

        UpdateHealthUI();

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + health.ToString();
    }

    void Die()
    {
        isDead = true;

        // Stop player input and movement
        if (inputScript != null) inputScript.move = Vector2.zero;
        if (movementScript != null) movementScript.enabled = false;

        if (deathAudio != null)
            deathAudio.Play();

        if (deathScreen != null)
            deathScreen.SetActive(true);

        if (playerCamera != null)
            StartCoroutine(ZoomOnDeath());

        StartCoroutine(RespawnPlayer());
    }

    IEnumerator ZoomOnDeath()
    {
        float startFOV = playerCamera.fieldOfView;
        float t = 0f;

        // Smooth camera zoom effect
        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            float progress = t / zoomDuration;
            playerCamera.fieldOfView = Mathf.Lerp(startFOV, zoomFOV, progress);
            yield return null;
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);

        // Reset values
        isDead = false;
        health = maxHealth;
        UpdateHealthUI();

        // Reset camera FOV
        if (playerCamera != null)
            playerCamera.fieldOfView = defaultFOV;

        // Move player back to checkpoint
        if (respawnPoint != null && characterController != null)
        {
            characterController.enabled = false;
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
            characterController.enabled = true;
        }

        // Re-enable player control
        if (movementScript != null)
            movementScript.enabled = true;

        if (deathScreen != null)
            deathScreen.SetActive(false);
    }
}
