
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotatingObstacle : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 60f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float rotationAmount =
            rotationSpeed * Time.fixedDeltaTime;

        Quaternion nextRotation =
            rb.rotation *
            Quaternion.Euler(0f, rotationAmount, 0f);

        rb.MoveRotation(nextRotation);
    }
}
