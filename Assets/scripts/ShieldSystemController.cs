using UnityEngine;

public class ShieldSystemController : MonoBehaviour
{
    public Transform holdPosition; // Assign this from player hand
    public float throwForce = 20f;
    public GameObject pickupPrompt; // UI text: "Press P to Pick Up"
    public GameObject throwPrompt; // UI text: "Press F to Throw"
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
        rb.isKinematic = true;
        shieldCollider.isTrigger = true;

        if (pickupPrompt) pickupPrompt.SetActive(false);
        if (throwPrompt) throwPrompt.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && !isHeld && Input.GetKeyDown(KeyCode.P))
        {
            PickUpShield();
        }

        if (isHeld && Input.GetKeyDown(KeyCode.F))
        {
            ThrowShield();
        }
    }

    private void PickUpShield()
    {
        isHeld = true;
        if (pickupPrompt) pickupPrompt.SetActive(false);

        // Snap to player's hand
        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        shieldCollider.isTrigger = false; // Disable trigger mode now
        if (pickupSound) pickupSound.Play();

        if (throwPrompt) throwPrompt.SetActive(true);
    }

    private void ThrowShield()
    {
        isHeld = false;

        transform.SetParent(null);
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Add forward force
        rb.AddForce(holdPosition.forward * throwForce, ForceMode.Impulse);
        if (throwSound) throwSound.Play();

        if (throwPrompt) throwPrompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (!isHeld && pickupPrompt) pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (pickupPrompt) pickupPrompt.SetActive(false);
        }
    }
}
