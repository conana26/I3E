using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CrystalClaim : MonoBehaviour
{
    public GameObject uiPrompt;
    public GameObject winUI;
    public GameObject crystalImage;
    public GameObject notEnoughUI;

    public AudioSource winSound;
    public AudioSource sadSound;

    private bool isPlayerNear = false;
    private bool hasBeenClaimed = false;

    void Update()
    {
        if (isPlayerNear && !hasBeenClaimed && Input.GetKeyDown(KeyCode.E))
        {
            int collected = SwordManager.instance.GetCollectedCount();
            int total = SwordManager.instance.totalSwords;

            if (collected >= total)
            {
                StartCoroutine(HandleCrystalClaim());
            }
            else
            {
                ShowNotEnoughUI();
            }
        }
    }

    IEnumerator HandleCrystalClaim()
    {
        hasBeenClaimed = true;

        if (uiPrompt) uiPrompt.SetActive(false);
        if (notEnoughUI) notEnoughUI.SetActive(false);

        // Show win UI and image
        if (winUI) winUI.SetActive(true);
        if (crystalImage) crystalImage.SetActive(true);

        // Play win sound
        float waitTime = 5f;
        if (winSound)
        {
            winSound.Play();
            waitTime = Mathf.Max(winSound.clip.length, waitTime);
        }

        // Shrink and disable crystal
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

        // Wait for full sound duration before hiding UI
        yield return new WaitForSeconds(waitTime);

        if (winUI) winUI.SetActive(false);
        if (crystalImage) crystalImage.SetActive(false);
    }

    void ShowNotEnoughUI()
    {
        if (uiPrompt) uiPrompt.SetActive(false);
        if (notEnoughUI) notEnoughUI.SetActive(true);
        if (sadSound) sadSound.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenClaimed)
        {
            isPlayerNear = true;
            if (uiPrompt) uiPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (uiPrompt) uiPrompt.SetActive(false);
        }
    }
}
