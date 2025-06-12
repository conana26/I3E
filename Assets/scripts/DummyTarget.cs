using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [Header("Sword Spawn")]
    public GameObject swordPrefab;       // <- assign the prefab, not scene object
    public Transform spawnPoint;

    [Header("Collapse FX")]
    public Animator animator;
    public AudioSource collapseSound;

    private bool isHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isHit) return;

        if (collision.gameObject.CompareTag("Shield"))
        {
            isHit = true;

            // Trigger collapse animation
            if (animator != null)
                animator.SetTrigger("Collapse");

            // Play collapse sound
            if (collapseSound != null)
                collapseSound.Play();

            // Spawn sword after a short delay to sync with animation
            Invoke("SpawnSword", 1.0f); // delay should match animation timing

            // Destroy dummy after animation finishes
            Destroy(gameObject, 3f);
        }
    }

    void SpawnSword()
    {
        if (swordPrefab != null && spawnPoint != null)
        {
            Instantiate(swordPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Sword prefab or spawn point not assigned.");
        }
    }
}
