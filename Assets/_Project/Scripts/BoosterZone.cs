
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoosterZone : MonoBehaviour
{
    [Header("Booster Settings")]
    [SerializeField] private float boostStrength = 5f;
    [SerializeField] private Transform direction;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        Rigidbody ballRb = other.attachedRigidbody;

        if (ballRb == null)
            return;

        if (ballRb.GetComponent<BallController>() == null)
            return;

        if (direction == null)
        {
            Debug.LogWarning("Booster direction is missing!", this);
            return;
        }

       
        Vector3 boostDirection = direction.forward;
        boostDirection.y = 0f;

      
        if (boostDirection.sqrMagnitude < 0.001f)
            return;

        boostDirection.Normalize();

        ballRb.AddForce(
            boostDirection * boostStrength,
            ForceMode.Impulse);
    }
}
