/* Creator: Lim Xue Zhi Conan
   Date of Creation: 11/6/25
   Script: 	Applies damage over time to player */
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damageAmount = 20;            // Amount of damage to apply per tick
    public float damageInterval = 1.0f;      // Time in seconds between each damage tick

    private float nextDamageTime = 0f;       // Tracks when the next damage can be applied

    private void OnTriggerStay(Collider other)
    {
        // Check if the object staying in the trigger is the player AND enough time has passed
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            // Try to get the PlayerHealth component from the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Apply damage to the player
                playerHealth.TakeDamage(damageAmount);

                // Set the next time damage can be applied
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
