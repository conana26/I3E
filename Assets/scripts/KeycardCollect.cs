using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeycardCollector : MonoBehaviour
{
    public GameObject collectPrompt;
    public GameObject keycardUIImage;
    public GameObject tempMessage;
    public AudioSource collectSound;
    public float messageDuration = 3f; // Renamed from WaitForSeconds

    private bool isPlayerNear = false;
    private bool isCollected = false;

    void Start()
    {
        if (collectPrompt) collectPrompt.SetActive(false);
        if (tempMessage) tempMessage.SetActive(false);
        if (keycardUIImage) keycardUIImage.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && !isCollected && Input.GetKeyDown(KeyCode.E))
        {
            CollectKeycard();
        }
    }

    void CollectKeycard()
    {
        isCollected = true;
        if (collectPrompt) collectPrompt.SetActive(false);

        StartCoroutine(ShrinkAndDisappear());

        if (collectSound) collectSound.Play();

        if (keycardUIImage) keycardUIImage.SetActive(true);

        if (tempMessage) StartCoroutine(ShowTempMessage());

        KeycardManager.hasKeycard = true;
    }

    IEnumerator ShrinkAndDisappear()
    {
        Vector3 originalScale = transform.localScale;
        float shrinkDuration = 0.5f;
        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsed / shrinkDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    IEnumerator ShowTempMessage()
    {
        tempMessage.SetActive(true);
        yield return new WaitForSeconds(messageDuration); // Correct usage
        tempMessage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isPlayerNear = true;
            if (collectPrompt) collectPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isPlayerNear = false;
            if (collectPrompt) collectPrompt.SetActive(false);
        }
    }
}
