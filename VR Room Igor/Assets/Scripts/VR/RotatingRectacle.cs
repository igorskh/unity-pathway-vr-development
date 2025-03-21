using UnityEngine;

public class RotatingRectacle : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new(0f, 20f, 0f);

    public void SetIsRotating(bool isRotating)
    {
        enabled = isRotating;
    }

    void FixedUpdate()
    {
        transform.Rotate(rotationSpeed*Time.fixedDeltaTime);
    }
}
