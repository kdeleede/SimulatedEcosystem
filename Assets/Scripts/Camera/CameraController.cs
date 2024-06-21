using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // A, D keys
        float moveVertical = Input.GetAxis("Vertical"); // W, S keys
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        // Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            RotateCamera(-rotationSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            RotateCamera(rotationSpeed);
        }
    }

    void RotateCamera(float rotationSpeed)
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = currentRotation;
    }
}
