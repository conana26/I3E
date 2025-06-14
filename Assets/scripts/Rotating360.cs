/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 10/6/25
   Script: Making the sword from parkour course rotate 360 degrees, and activate from pressing "T"*/
using UnityEngine;

public class Rotating360 : MonoBehaviour
{
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 360); // Defines smooth curve from 0 to 360 degrees
    public float rotationDuration = 1.5f; // Time taken to complete one rotation
    public KeyCode rotateKey = KeyCode.T; // Key to press to trigger rotation

    private bool isRotating = false;      // Tracks if currently rotating
    private float rotationTimer = 0f;     // Time since rotation started
    private float startRotationY;         // Initial Y rotation when starting

    void Update()
    {
        // Start rotation on key press
        if (Input.GetKeyDown(rotateKey) && !isRotating)
        {
            StartRotation();
        }

        // If already rotating, update rotation
        if (isRotating)
        {
            UpdateRotation();
        }
    }

    // Initializes rotation state
    void StartRotation()
    {
        isRotating = true;
        rotationTimer = 0f;
        startRotationY = transform.eulerAngles.y;
    }

    // Updates rotation based on time elapsed
    void UpdateRotation()
    {
        rotationTimer += Time.deltaTime;
        float progress = rotationTimer / rotationDuration;

        // Clamp progress at 1 to end rotation
        if (progress >= 1f)
        {
            progress = 1f;
            isRotating = false;
        }

        // Evaluate the curve to get current rotation value
        float rotationValue = rotationCurve.Evaluate(progress);
        transform.rotation = Quaternion.Euler(0, startRotationY + rotationValue, 0);
    }
}
