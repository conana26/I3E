/* 
 Creator: Lim Xue Zhi Conan
 Date Of Creation: 13/6/25
 Script: Handles dummy target destruction when hit by the thrown shield 
*/

using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [Header("Sword Spawn")]
    public GameObject swordPrefab;       // The prefab of the sword to spawn upon destruction (must be assigned in Inspector)
    public Transform spawnPoint;         // The position and rotation where the sword will appear

    [Header("Collapse FX")]
    public Animator animator;            // Animator controlling the dummy's collapse animation
    public AudioSource collapseSound;    // Sound to play when the dummy is hit

    private bool isHit = false;          // Flag to prevent multiple hits from triggering duplicate effects

    private void OnCollisionEnter(Collision collision)
    {
        // If already hit before, do nothing to prevent double activation
        if (isHit) return;

        // Check if the colliding object is tagged as "Shield"
        if (collision.gameObject.CompareTag("Shield"))
        {
            isHit = true; // Mark this dummy as hit

            // Play the collapse animation, if Animator is assigned
            if (animator != null)
                animator.SetTrigger("Collapse");

            // Play the collapse sound, if AudioSource is assigned
            if (collapseSound != null)
                collapseSound.Play();

            // Schedule the sword to spawn 1 second later (after animation)
            Invoke("SpawnSword", 1.0f);

            // Destroy the dummy object after 3 seconds to allow animations/sounds to finish
            Destroy(gameObject, 3f);
        }
    }

     void SpawnSword()
    {
        // Only proceed if the prefab and spawn point are properly set
        if (swordPrefab != null && spawnPoint != null)
        {
            // Instantiate the sword at the spawn point's position and rotation
            Instantiate(swordPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            // Warn in the console if something is missing
            Debug.LogWarning("Sword prefab or spawn point not assigned.");
        }
    }
}
