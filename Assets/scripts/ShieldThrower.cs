using UnityEngine;

public class ShieldThrower : MonoBehaviour
{
    public GameObject heldShield;      // Drag your shield prefab here (after pickup)
    public float throwForce = 20f;

    public AudioSource audioSource;
    public AudioClip throwSound;

    private bool hasShield = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasShield && heldShield != null)
        {
            // Unparent the shield
            heldShield.transform.SetParent(null);

            // Enable physics
            Rigidbody rb = heldShield.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.linearVelocity = transform.forward * throwForce;

            // Re-enable collider
            heldShield.GetComponent<Collider>().enabled = true;

            // Play throw sound
            if (audioSource && throwSound)
                audioSource.PlayOneShot(throwSound);

            // Reset
            heldShield = null;
            hasShield = false;
        }
    }

    // This method can be called by your ShieldPickup script
    public void AttachShield(GameObject shield)
    {
        heldShield = shield;
        hasShield = true;
    }
}

