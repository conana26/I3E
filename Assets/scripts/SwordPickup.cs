using System.Collections;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public GameObject interactionUI; // the "Press E" text
    public GameObject swordObject;   // the actual sword model
    public AudioSource pickupSound;  // sound to play
    private bool playerInRange = false;
    private bool isCollected = false;

    void Start()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }

    void Update()
    {
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

        // Animate sword shrinking
        float shrinkTime = 0.3f;
        Vector3 originalScale = swordObject.transform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / shrinkTime;
            swordObject.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            yield return null;
        }

        SwordManager.instance.CollectSword();
        Destroy(transform.parent.gameObject); // destroy full sword object
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionUI != null)
                interactionUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionUI != null)
                interactionUI.SetActive(false);
        }
    }
}
