using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallShooter : MonoBehaviour
{
    [Header("Shot Settings")]
    public float shootForce = 200f;

    [HideInInspector] public Rigidbody rb;

    private bool hasShot = false;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        if (hasShot) return;

        if (Input.GetKeyDown(KeyCode.A))
            Shoot(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.W))
            Shoot(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.D))
            Shoot(Vector3.right);
    }

    void Shoot(Vector3 direction)
    {
        if (rb == null)
        {
            Debug.LogError("BallShooter: No Rigidbody attached!");
            return;
        }

        hasShot = true;
        rb.isKinematic = false;
        rb.AddForce(-1 * (Vector3.forward + direction * 0.49f + Vector3.down * 0.7f) * shootForce, ForceMode.Impulse);

        // Trigger goalie dive
        GoalieAI goalie = FindObjectOfType<GoalieAI>();
        if (goalie != null)
        {
            goalie.DiveRandomly();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasShot) return;
    }
}
