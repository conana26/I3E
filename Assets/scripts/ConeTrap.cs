using UnityEngine;

public class ConeTrap : MonoBehaviour
{
    public float moveDistance = 5f; // How far down from starting position
    public float moveSpeed = 1f; // Speed of oscillation
    
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        float yOffset = (Mathf.Sin(Time.time * moveSpeed) + 1f) * 0.5f * moveDistance;
        transform.position = new Vector3(startPosition.x, startPosition.y - yOffset, startPosition.z);
    }
}
