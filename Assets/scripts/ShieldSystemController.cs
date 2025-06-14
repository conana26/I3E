/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 13/6/25
   Script: Shield system - pickup with 'P' and throw with 'F' */

using UnityEngine;

public class ShieldSystemController : MonoBehaviour
{
    public Transform holdPosition;          // The hand position to attach shield to
    public float throwForce = 20f;          // How fast the shield is thrown
    public GameObject pickupPrompt;         // UI prompt: "Press P to Pick Up"
    public GameObject throwPrompt;          // UI prompt: "Press F to Throw"
    public AudioSource pickupSound;
    public AudioSource throwSound;

    private bool isHeld = false;
    private Rigidbody rb;
    private Collider shieldCollider;
    private bool isPlayerNear = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shieldCollider = GetComponent<Collider>();

        // Shield starts as a pickup item
        rb.isKinematic = true;
        shieldCollider.isTrigger = true;

        // Hide UI prompts initially
        if (pickupPrompt) pickupPrompt.SetActive(false);
        if (throwPrompt) throwPrompt.SetActive(false);
    }

    void Update()
    {
        // Pickup when player is near and presses P
        if (isPlayerNear && !isHeld && Input.GetKeyDown(KeyCode.P))
        {
            PickUpShield();
        }

        // Throw when player is holding it and presses F
        if (isHeld && Input.GetKeyDown(KeyCode.F))
        {
            ThrowShield();
        }
    }

    private void PickUpShield()
    {
        isHeld = true;
        if (pickupPrompt) pickupPrompt.SetActive(false);

        // Attach shield to hand
        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Disable physics while held
        rb.isKinematic = true;
        shieldCollider.isTrigger = false;

        if (pickupSound) pickupSound.Play();
        if (throwPrompt) throwPrompt.SetActive(true);
    }

    private void ThrowShield()
    {
        isHeld = false;

        // Detach from hand
        transform.SetParent(null);
        rb.isKinematic = false;

        // Reset velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Throw forward
        rb.AddForce(holdPosition.forward * throwForce, ForceMode.Impulse);

        if (throwSound) throwSound.Play();
        if (throwPrompt) throwPrompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (!isHeld && pickupPrompt)
                pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            if (pickupPrompt)
                pickupPrompt.SetActive(false);
        }
    }
}
