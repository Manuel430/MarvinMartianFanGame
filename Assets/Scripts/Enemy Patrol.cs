using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrolling")]
    [SerializeField] float patrolSpeed;
    [SerializeField] float rayLength;
    [SerializeField] float rayGroundLength;
    [SerializeField] LayerMask obstacleLayers;

    Rigidbody2D rBody;

    public void Stop()
    {
        patrolSpeed = 0;
    }

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if(IsFacingRight())
        {
            rBody.linearVelocity = new Vector2(patrolSpeed, rBody.linearVelocityY);
        }
        else
        {
            rBody.linearVelocity = new Vector2(-patrolSpeed, rBody.linearVelocityY);
        }

        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 1);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * (IsFacingRight() ? 1 : -1), rayLength, obstacleLayers);
        Debug.DrawRay(transform.position, Vector2.right * (IsFacingRight() ? rayLength : -rayLength), Color.red);

        Vector2 rayGroundOrigin = new Vector2(transform.position.x + (IsFacingRight() ? 1 : -1), transform.position.y);
        RaycastHit2D hitGround = Physics2D.Raycast(rayGroundOrigin, Vector2.down, rayGroundLength, obstacleLayers);
        Debug.DrawRay(rayGroundOrigin, Vector2.down * rayGroundLength, Color.cyan);

        if (hit.collider != null || hitGround.collider == null)
        {
            Turn();
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    void Turn()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Turn();
        }
    }
}
