using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
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

                    playerHealth.TakeDamage(5);
            }
            //Knockback
        }
    }
}
