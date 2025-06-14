using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class KeycardDoor : MonoBehaviour
{
    [Header("UI Prompts")]
    public GameObject useKeycardPrompt;      // "Press U to Use Keycard"
    public GameObject openDoorPrompt;        // "Press E to Open Door"
    public GameObject needKeycardPrompt;     // "You need a Keycard." <-- new

    [Header("Audio")]
    public AudioSource unlockSound;
    public AudioSource openSound;
    public AudioSource lockedDoorSound;      // <-- new

    private bool isPlayerNear = false;
    private bool isUnlocked = false;
    private bool isOpen = false;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (useKeycardPrompt) useKeycardPrompt.SetActive(false);
        if (openDoorPrompt) openDoorPrompt.SetActive(false);
        if (needKeycardPrompt) needKeycardPrompt.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && !isUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (KeycardManager.hasKeycard)
                {
                    UnlockDoor();
                }
                else
                {
                    // Player tried to use keycard without having one
                    if (lockedDoorSound) lockedDoorSound.Play();
                    if (needKeycardPrompt) StartCoroutine(ShowTemporaryPrompt(needKeycardPrompt, 2f));
                }
            }
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
        if (animator)
        {
            animator.Play("DoorOpen", 0, 0.0f);
            animator.SetTrigger("Open");
        }
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

    IEnumerator ShowTemporaryPrompt(GameObject prompt, float duration)
    {
        prompt.SetActive(true);
        yield return new WaitForSeconds(duration);
        prompt.SetActive(false);
    }
}
