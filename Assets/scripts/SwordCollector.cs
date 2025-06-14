/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 11/6/25
   Script: Automatically collects sword when player touches it */
using UnityEngine;

public class SwordCollector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // When player collides, count the sword and destroy it
        if (other.CompareTag("Player"))
        {
            SwordManager.instance.CollectSword(); // Add to count
            Destroy(gameObject);                  // Remove from scene
        }
    }
}
