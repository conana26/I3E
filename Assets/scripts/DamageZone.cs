using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damageAmount = 20;
    public float damageInterval = 1.0f;

    private float nextDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
