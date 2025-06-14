/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 12/6/25
   Script: Displays a UI popup when the player enters a specific trigger area */
using UnityEngine;
using TMPro;

public class PlatformPopup : MonoBehaviour
{
    [Header("UI References")]
    public GameObject uiPopup; // Assign the UI GameObject (Canvas or Text) in Inspector
    public TextMeshProUGUI textComponent; // Assign the TextMeshProUGUI element in Inspector

    [Header("Popup Settings")]
    [TextArea]
    public string popupMessage = "Press \"T\" to rotate sword.";

    private void Start()
    {
        if (uiPopup != null)
            uiPopup.SetActive(false);

        if (textComponent != null && !string.IsNullOrEmpty(popupMessage))
            textComponent.text = popupMessage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideUI();
        }
    }

    private void ShowUI()
    {
        if (uiPopup != null)
            uiPopup.SetActive(true);
    }

    private void HideUI()
    {
        if (uiPopup != null)
            uiPopup.SetActive(false);
    }
}
