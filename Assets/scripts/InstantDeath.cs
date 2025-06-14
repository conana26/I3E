/* 
 Creator: Lim Xue Zhi Conan
 Date Of Creation: 12/6/25
 Script: Instantly kills the player upon contact with death zone 
*/

using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the player
        if (other.CompareTag("Player"))
        {
            // Try to get the PlayerHealth component from the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Deal damage equal to the player's full health, effectively killing them instantly
                playerHealth.TakeDamage(playerHealth.health);
            }
        }
    }
}
