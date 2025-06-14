/* Creator: Lim Xue Zhi Conan
   Date Of Creation: 10/6/25
   Script: For objects that move left and right (e.g. the stairs from parkour course) */
using UnityEngine;

public class LeftRightMovement : MonoBehaviour
{
    public float moveDistance = 3f; // How far to move left/right
    public float moveSpeed = 2f;    // Speed of movement
    public bool moveLeft = true;    // Toggle to determine movement direction

    private Vector3 startPosition;  // Original position of the platform/trap

    void Start()
    {
        startPosition = transform.position; // Store starting position
    }

    void Update()
    {
        // Calculate horizontal offset using PingPong (oscillates between 0 and moveDistance)
        float xOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        // Apply horizontal movement in selected direction
        if (moveLeft)
            transform.position = new Vector3(startPosition.x - xOffset, startPosition.y, startPosition.z);
        else
            transform.position = new Vector3(startPosition.x + xOffset, startPosition.y, startPosition.z);
    }
}
