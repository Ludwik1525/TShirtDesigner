using UnityEngine;

public class Rotation : MonoBehaviour
{
    float rotationSpeed = 0.2f;

    void OnMouseDrag()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        // select the axis by which you want to rotate the GameObject
        transform.RotateAround(Vector3.down, XaxisRotation);
    }
}
