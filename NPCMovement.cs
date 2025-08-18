using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentWaypointIndex = 0;
    private bool isInteracting = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isInteracting)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);

        bool isMoving = Vector2.Distance(transform.position, targetWaypoint.position) >= 0.1f;
        animator.SetBool("isMoving", isMoving);

        if (!isMoving)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    public void StartInteraction()
    {
        isInteracting = true;
    }

    public void EndInteraction()
    {
        isInteracting = false;
    }

    public void ResumeAfterDialog()
    {
        isInteracting = false;
    }
}
