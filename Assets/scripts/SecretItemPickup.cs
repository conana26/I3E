using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecretItemPickup : MonoBehaviour
{
    public GameObject interactionUI;      // "Press E to collect" prompt
    public GameObject secretItemUI;       // Permanent UI showing "Secret Item: Axe"
    public GameObject axeModel;           // The 3D model to shrink and disappear
    public AudioSource pickupSound;

    private bool isPlayerNearby = false;
    private bool isCollected = false;

    void Update()
    {
        if (isPlayerNearby && !isCollected && Input.GetKeyDown(KeyCode.E))
        {
            CollectAxe();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionUI) interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionUI) interactionUI.SetActive(false);
        }
    }

    void CollectAxe()
    {
        isCollected = true;

        if (interactionUI) interactionUI.SetActive(false);
        if (secretItemUI) secretItemUI.SetActive(true);

        if (pickupSound)
        {
            pickupSound.Stop();
            pickupSound.Play();
        }

        if (axeModel != null)
        {
            StartCoroutine(ShrinkAndDisableModel());
        }
    }

    IEnumerator ShrinkAndDisableModel()
    {
        float duration = 0.5f;
        float time = 0f;
        Vector3 originalScale = axeModel.transform.localScale;

        while (time < duration)
        {
            axeModel.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        axeModel.transform.localScale = Vector3.zero;
        axeModel.SetActive(false);
    }
}
