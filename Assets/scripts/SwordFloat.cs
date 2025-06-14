/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 10/6/25
   Script: Makes sword collectible rotate and bounce */
using UnityEngine;

public class SwordFloat : MonoBehaviour
{
    public float floatSpeed = 2f;         // Speed at which the sword floats up and down
    public float floatHeight = 0.25f;     // Maximum height of the float
    public float rotationSpeed = 50f;     // Rotation speed around Y-axis

    private Vector3 startPos;             // Original position of the sword

    void Start()
    {
        startPos = transform.position;    // Save starting position
    }

    void Update()
    {
        // Continuously rotate the sword around Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Move the sword up and down using a sine wave
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
