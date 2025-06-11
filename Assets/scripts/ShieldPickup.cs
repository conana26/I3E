using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public Transform holdPoint; // Drag player's hand point here
    private bool isInRange = false;
    private GameObject player;

    void Update()
{
    if (isInRange && Input.GetKeyDown(KeyCode.P))
    {
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;

        // Tell the ShieldThrower
        ShieldThrower thrower = player.GetComponentInChildren<ShieldThrower>();
        if (thrower != null)
        {
            thrower.AttachShield(gameObject);
        }
    }
}


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
