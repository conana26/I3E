/* Creator: Lim Xue Zhi Conan
   Date of Creation: 14/6/25
   Script: Unlocks and opens door with keycard*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeycardDoor : MonoBehaviour
{
    [Header("UI Prompts")]
    public GameObject useKeycardPrompt;      // "Press U to Use Keycard"
    public GameObject openDoorPrompt;        // "Press E to Open Door"
    public GameObject needKeycardPrompt;     // "You need a Keycard."

    [Header("Audio")]
    public AudioSource unlockSound;
    public AudioSource openSound;
    public AudioSource lockedDoorSound;      // Played if player tries to unlock without keycard

    private bool isPlayerNear = false;       // Tracks if player is near the door
    private bool isUnlocked = false;         // Tracks if the door has been unlocked
    private bool isOpen = false;             // Tracks if the door has been opened

    public Animator animator;                // Animator component for door animation

    void Start()
    {
        // Get Animator component attached to the door
        animator = GetComponent<Animator>();

        // Ensure all prompts are hidden when the game starts
        if (useKeycardPrompt) useKeycardPrompt.SetActive(false);
        if (openDoorPrompt) openDoorPrompt.SetActive(false);
        if (needKeycardPrompt) needKeycardPrompt.SetActive(false);
    }

    void Update()
    {
        // If player is near and door is not yet unlocked
        if (isPlayerNear && !isUnlocked)
        {
            // Player presses 'U' to use the keycard
            if (Input.GetKeyDown(KeyCode.U))
            {
                // If player has the keycard, unlock the door
                if (KeycardManager.hasKeycard)
                {
                    UnlockDoor();
                }
                else
                {
                    // If not, play locked sound and show "need keycard" prompt temporarily
                    if (lockedDoorSound) lockedDoorSound.Play();
                    if (needKeycardPrompt) StartCoroutine(ShowTemporaryPrompt(needKeycardPrompt, 2f));
                }
            }
        }

        // If player is near, door is unlocked, and not open yet
        // Player presses 'E' to open the door
        if (isPlayerNear && isUnlocked && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    void UnlockDoor()
    {
        isUnlocked = true; // Mark door as unlocked

        // Hide the keycard prompt and show the open door prompt
        if (useKeycardPrompt) useKeycardPrompt.SetActive(false);
        if (openDoorPrompt) openDoorPrompt.SetActive(true);

        // Play unlock sound
        if (unlockSound) unlockSound.Play();
    }

    void OpenDoor()
    {
        isOpen = true; // Mark door as opened

        // Play door opening sound
        if (openSound) openSound.Play();

        // Trigger door opening animation
        if (animator)
        {
            animator.Play("DoorOpen", 0, 0.0f);   // Play animation from the beginning
            animator.SetTrigger("Open");         // Set animation trigger
        }

        // Hide the open door prompt
        if (openDoorPrompt) openDoorPrompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            // Show appropriate prompt based on door state
            if (!isUnlocked && KeycardManager.hasKeycard)
            {
                if (useKeycardPrompt) useKeycardPrompt.SetActive(true);
            }
            else if (isUnlocked && !isOpen)
            {
                if (openDoorPrompt) openDoorPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When player leaves the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            // Hide all prompts
            if (useKeycardPrompt) useKeycardPrompt.SetActive(false);
            if (openDoorPrompt) openDoorPrompt.SetActive(false);
        }
    }

    IEnumerator ShowTemporaryPrompt(GameObject prompt, float duration)
    {
        // Show prompt for a limited time then hide it
        prompt.SetActive(true);
        yield return new WaitForSeconds(duration);
        prompt.SetActive(false);
    }
}
