using UnityEngine;

public class LeftRightMovement : MonoBehaviour
{
    public float moveDistance = 3f; // How far to move
    public float moveSpeed = 2f; // Speed of movement
    public bool moveLeft = true; // Check this for left movement, uncheck for right
    
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        float xOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        
        if (moveLeft)
            transform.position = new Vector3(startPosition.x - xOffset, startPosition.y, startPosition.z);
        else
            transform.position = new Vector3(startPosition.x + xOffset, startPosition.y, startPosition.z);
    }
}