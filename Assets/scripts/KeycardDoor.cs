using UnityEngine;
using UnityEngine.UI;

public class KeycardDoor : MonoBehaviour
{
    public GameObject useKeycardPrompt; // "Press U to Use Keycard"
    public GameObject openDoorPrompt;   // "Press E to Open Door"
    public AudioSource unlockSound;
    public AudioSource openSound;

    private bool isPlayerNear = false;
    private bool isUnlocked = false;
    private bool isOpen = false;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (useKeycardPrompt) useKeycardPrompt.SetActive(false);
        if (openDoorPrompt) openDoorPrompt.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && !isUnlocked && Input.GetKeyDown(KeyCode.U) && KeycardManager.hasKeycard)
        {
            UnlockDoor();
        }

        if (isPlayerNear && isUnlocked && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    void UnlockDoor()
    {
        isUnlocked = true;
        if (useKeycardPrompt) useKeycardPrompt.SetActive(false);
        if (openDoorPrompt) openDoorPrompt.SetActive(true);
        if (unlockSound) unlockSound.Play();
    }

    void OpenDoor()
    {
        isOpen = true;
        if (openSound) openSound.Play();
        animator?.Play("DoorOpen", 0, 0.0f);
        if (animator) animator.SetTrigger("Open");
        if (openDoorPrompt) openDoorPrompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
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
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (useKeycardPrompt) useKeycardPrompt.SetActive(false);
            if (openDoorPrompt) openDoorPrompt.SetActive(false);
        }
    }
}

