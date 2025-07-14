using UnityEngine;

public class SecretGoal : MonoBehaviour
{
    [Header("OnTriggerEnter")]
    [SerializeField] GameObject marvinRest;

    [Header("UI")]
    [SerializeField] GameObject transitionUI;
    [SerializeField] GameObject nextLevel;

    public void TransitionUI()
    {
        transitionUI.SetActive(true);
    }

    public void NextLevelUI()
    {
        nextLevel.SetActive(true);
        transitionUI.SetActive(false);
    }

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
            if (playerAnim != null) { playerAnim.IsInvisible(); }

            marvinRest.SetActive(true);
        }
    }
}
