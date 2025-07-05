using UnityEngine;

public class EndGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponentInParent<PlayerMovement>();
            if (playerMovement != null) { playerMovement.StopMoving(); }

            Blaster playerBlaster = collision.GetComponentInParent<Blaster>();
            if (playerBlaster != null) { playerBlaster.StopShooting(true); }

            PlayerUI playerUI = collision.GetComponentInParent<PlayerUI>();
            if (playerUI != null) { playerUI.CannotPause(); }

            PlayerAnimation playerAnim = collision.GetComponent<PlayerAnimation>();
            if (playerAnim != null) { playerAnim.HasWon(); }

            Destroy(gameObject);
        }
    }
}
