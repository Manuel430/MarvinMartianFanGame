using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int damageDealt;
    [SerializeField] bool isAmmo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            if(playerHealth != null && playerMovement != null)
            {
                playerMovement.SetKBTimer();
                if(collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.GetKnockbackPush(true);
                }
                else
                {
                    playerMovement.GetKnockbackPush(false);
                }

                    playerHealth.TakeDamage(damageDealt);
            }

            if (isAmmo)
            {
                Debug.Log(collision);
                Destroy(gameObject);
            }
        }

        if (isAmmo)
        {
            if (collision.gameObject.layer == 6 || collision.gameObject.layer == 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
