using UnityEngine;

public class Rotating360 : MonoBehaviour
{
   
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 360);
    public float rotationDuration = 1.5f;
    public KeyCode rotateKey = KeyCode.T;
    
    private bool isRotating = false;
    private float rotationTimer = 0f;
    private float startRotationY;
    
    void Update()
    {
        if (Input.GetKeyDown(rotateKey) && !isRotating)
        {
            StartRotation();
        }
        
        if (isRotating)
        {
            UpdateRotation();
        }
    }
    
    void StartRotation()
    {
        isRotating = true;
        rotationTimer = 0f;
        startRotationY = transform.eulerAngles.y;
    }
    
    void UpdateRotation()
    {
        rotationTimer += Time.deltaTime;
        float progress = rotationTimer / rotationDuration;
        
        if (progress >= 1f)
        {
            progress = 1f;
            isRotating = false;
        }
        
        float rotationValue = rotationCurve.Evaluate(progress);
        transform.rotation = Quaternion.Euler(0, startRotationY + rotationValue, 0);
    }
   
}
