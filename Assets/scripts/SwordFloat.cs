/*Creator: Lim Xue Zhi Conan
Date Of Creation: 10/6/25
Script: Making Sword Collectible rotate and bounce*/
using UnityEngine;

public class SwordFloat : MonoBehaviour
{
    public float floatSpeed = 2f;         // Speed of the bobbing
    public float floatHeight = 0.25f;     // How high it bobs
    public float rotationSpeed = 50f;     // Speed of rotation

    private Vector3 startPos;

    void Start()
    {
        // Save the original position
        startPos = transform.position;
    }

    void Update()
    {
        // Rotate around its own Y axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Bounce up and down smoothly
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}

