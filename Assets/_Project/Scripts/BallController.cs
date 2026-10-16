
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class BallController : MonoBehaviour
{
    [Header("Shot Settings")]
    [SerializeField] private float shotPower = 3f;
    [SerializeField] private float maxDragDistance = 4f;

    [Header("Movement Settings")]
    [SerializeField] private float stopThreshold = 0.1f;

    private Rigidbody rb;
    private LineRenderer shotIndicator;
    private Camera mainCamera;

    private bool isDragging;
    private Vector3 dragPosition;

    private bool hasPendingShot;
    private Vector3 pendingImpulse;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        shotIndicator = GetComponent<LineRenderer>();
        mainCamera = Camera.main;

        shotIndicator.positionCount = 2;
        shotIndicator.enabled = false;
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            UpdateDragging();
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            ReleaseShot();
        }
    }

    
    private void FixedUpdate()
    {
        if (hasPendingShot)
        {
            rb.AddForce(pendingImpulse, ForceMode.Impulse);
            hasPendingShot = false;
            return;
        }

       
        if (!isDragging &&
            rb.linearVelocity.magnitude < stopThreshold)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void StartDragging()
    {
        if (Time.timeScale == 0f)
            return;

        if (GameManager.Instance.IsLevelCompleted)
            return;

        if (!IsBallStopped() || hasPendingShot)
            return;

        Ray ray = mainCamera.ScreenPointToRay(
            Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.rigidbody == rb)
            {
                isDragging = true;
                dragPosition = transform.position;

                shotIndicator.enabled = true;
            }
        }
    }

    private void UpdateDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(
            Input.mousePosition);

        Plane plane = new Plane(
            Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mousePosition =
                ray.GetPoint(distance);

            Vector3 drag =
                mousePosition - transform.position;

            drag = Vector3.ClampMagnitude(
                drag, maxDragDistance);

            dragPosition = transform.position + drag;

            shotIndicator.SetPosition(
                0, transform.position);

            shotIndicator.SetPosition(
                1, dragPosition);
        }
    }

    private void ReleaseShot()
    {
        isDragging = false;
        shotIndicator.enabled = false;

        Vector3 shotDirection =
            transform.position - dragPosition;

        shotDirection.y = 0f;

        if (shotDirection.magnitude < 0.05f)
            return;

        pendingImpulse = shotDirection * shotPower;
        hasPendingShot = true;
        GameManager.Instance.RegisterShot();
    }
   
    private bool IsBallStopped()
    {
        return rb.linearVelocity.magnitude < stopThreshold;
    }
}
