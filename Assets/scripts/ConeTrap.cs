/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 10/6/25
   Script: Making Cones move up and down from parkour course*/
using UnityEngine;

public class ConeTrap : MonoBehaviour
{
    public float moveDistance = 5f; // Distance the trap moves down
    public float moveSpeed = 1f;    // Speed of the up-down movement

    private Vector3 startPosition;  // Starting position of the trap

    void Start()
    {
        startPosition = transform.position; // Store original position
    }

    void Update()
    {
        // Calculate vertical offset using sine wave for smooth oscillation
        float yOffset = (Mathf.Sin(Time.time * moveSpeed) + 1f) * 0.5f * moveDistance;

        // Apply vertical movement (downward)
        transform.position = new Vector3(startPosition.x, startPosition.y - yOffset, startPosition.z);
    }
}
