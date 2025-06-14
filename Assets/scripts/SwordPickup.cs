/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 11/6/25
   Script: Interactable sword pickup that shrinks and disappears */
using System.Collections;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public GameObject interactionUI; // UI that shows "Press E"
    public GameObject swordObject;   // Sword model to shrink
    public AudioSource pickupSound;  // Sound effect on pickup

    private bool playerInRange = false;
    private bool isCollected = false;

    void Start()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false); // Hide UI at start
    }

    void Update()
    {
        // If player presses E while near, collect the sword
        if (playerInRange && !isCollected && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CollectSword());
        }
    }

    IEnumerator CollectSword()
    {
        isCollected = true;

        if (interactionUI != null)
            interactionUI.SetActive(false);

        if (pickupSound != null)
            pickupSound.Play();

        // Smoothly shrink the sword
        float shrinkTime = 0.3f;
        Vector3 originalScale = swordObject.transform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / shrinkTime;
            swordObject.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            yield return null;
        }

        SwordManager.instance.CollectSword();         // Increase count
        Destroy(transform.parent.gameObject);         // Destroy full object
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionUI != null)
                interactionUI.SetActive(true); // Show prompt
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionUI != null)
                interactionUI.SetActive(false); // Hide prompt
        }
    }
}
