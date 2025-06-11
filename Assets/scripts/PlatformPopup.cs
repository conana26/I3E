using UnityEngine;
using TMPro;

public class PlatformPopup : MonoBehaviour
{
    [Header("UI References")]
    public GameObject uiPopup; // Drag your UI popup GameObject here
    
    [Header("Settings")]
    public string popupMessage = "Press \"T\" to rotate sword.";
    
    void Start()
    {
        // Make sure UI is hidden at start
        if (uiPopup != null)
            uiPopup.SetActive(false);
        
        // Set the popup text if there's a TextMeshPro component
        TextMeshProUGUI textComponent = uiPopup.GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
            textComponent.text = popupMessage;
        
        // Or if TextMeshPro is a child of the popup
        TextMeshProUGUI childText = uiPopup.GetComponentInChildren<TextMeshProUGUI>();
        if (childText != null)
            childText.text = popupMessage;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowUI();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideUI();
        }
    }
    
    void ShowUI()
    {
        if (uiPopup != null)
            uiPopup.SetActive(true);
    }
    
    void HideUI()
    {
        if (uiPopup != null)
            uiPopup.SetActive(false);
    }
}