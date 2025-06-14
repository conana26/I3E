/*Creator: Lim Xue Zhi Conan
Date Of Creation: 10/6/25
Script: Making the door open with lever*/
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;  // Reference to the door's Animator

    [SerializeField] private bool openTrigger = false;  // Optional: can be used to customize open behavior
    [SerializeField] private bool closeTrigger = false; // Optional: can be used to customize close behavior

    // Called when something enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // If the player steps in
        {
            myDoor.Play("DoorOpen", 0, 0.0f); // Play the "DoorOpen" animation from start
            gameObject.SetActive(false);      // Disable this trigger to prevent re-triggering
        }
        else if (closeTrigger) // If not the player, and closing is enabled
        {
            myDoor.Play("DoorClose", 0, 0.0f); // Play the "DoorClose" animation from start
            gameObject.SetActive(false);       // Disable trigger
        }
    }
}
