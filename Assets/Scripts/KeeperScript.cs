using UnityEngine;

public class GoalieAI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float diveRotationAngle = 45f; // How much to rotate when diving
    public float diveVerticalOffset = 0.5f; // How much to rise when diving
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool isDiving = false;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        targetPosition = transform.position;
        targetRotation = startRotation;
    }

    public void DiveRandomly()
    {
        int dir = Random.Range(0, 3); // 0=left, 1=center, 2=right
        if (dir == 0)
        {
            targetPosition = startPosition + Vector3.left * 3f + Vector3.up * diveVerticalOffset;
            targetRotation = startRotation * Quaternion.Euler(0, 0, diveRotationAngle); // Rotate left
            isDiving = true;
        }
        else if (dir == 1)
        {
            targetPosition = startPosition;
            targetRotation = startRotation; // Stay upright
            isDiving = false;
        }
        else
        {
            targetPosition = startPosition + Vector3.right * 3f + Vector3.up * diveVerticalOffset;
            targetRotation = startRotation * Quaternion.Euler(0, 0, -diveRotationAngle); // Rotate right
            isDiving = true;
        }
    }

    void Update()
    {
        // Smoothly move to target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Smoothly rotate to target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, moveSpeed * 30f * Time.deltaTime);

        // Return to upright position when not diving and reached target
        if (!isDiving && Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetRotation = startRotation;
        }
    }
}