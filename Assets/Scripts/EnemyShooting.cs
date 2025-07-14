using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Outside Inputs")]
    [SerializeField] Transform firepoint;
    [SerializeField] GameObject ammo;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] EnemyAnimation enemyAnim;

    [Header("Shooting")]
    [SerializeField] float shootCooldown;
    float shootTime;
    bool willShoot;

    private void Awake()
    {
        shootTime = 0;
    }

    private void Update()
    {
        if (willShoot)
        {
            shootTime -= Time.deltaTime;
            if (shootTime <= 0)
            {
                shootTime = 0;
                Shoot();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Shoot();
            enemyAnim.IsShooting(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            willShoot = false;
            shootTime = shootCooldown;
            enemyAnim.IsShooting(false);
        }
    }

    private void Shoot()
    {
        Instantiate(ammo, firepoint.position, firepoint.rotation);
        shootTime = shootCooldown;
        willShoot = true;
    }
}
