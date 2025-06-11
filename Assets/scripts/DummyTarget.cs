using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public GameObject swordCollectiblePrefab;
    public Transform spawnPoint;

    public GameObject collapseEffect; // Optional: Dummy collapsing animation or particles
    public AudioClip hitSound;
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Shield"))
        {
          Debug.Log("HIT by shield!");

        // Your remaining logic...
        }
        if (collision.gameObject.CompareTag("Shield"))
        {

            // Play sound
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            // Optional collapse visual
            if (collapseEffect != null)
            {
                Instantiate(collapseEffect, transform.position, Quaternion.identity);
            }

            // Spawn collectible sword
            if (swordCollectiblePrefab != null && spawnPoint != null)
            {
                Instantiate(swordCollectiblePrefab, spawnPoint.position, Quaternion.identity);
            }

            Destroy(gameObject); // Remove dummy
        }
    }
}
