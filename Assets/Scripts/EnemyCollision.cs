using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Health playerhealth = collision.gameObject.GetComponent<Health>();
            if(playerhealth != null)
            {
                playerhealth.TakeDamage(5);
            }
            //Knockback
        }
    }
}
