using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Outside Inputs")]
    [SerializeField] Transform firepoint;
    [SerializeField] GameObject ammo;
    [SerializeField] LayerMask groundLayer;
    Animator enemyAnimator;

    [Header("Shooting")]
    [SerializeField] float shootCooldown;
    float shootTime;
    bool willShoot;

    private void Update()
    {
        --shootTime;
        if(willShoot && shootTime <= 0)
        {
            shootTime = 0;
            //Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in Range");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player out of Range");
        }
    }

    private void Shoot()
    {
        
    }
}
