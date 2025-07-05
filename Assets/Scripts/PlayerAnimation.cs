using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Outside Inputs")]
    [SerializeField] PlayerMovement movement;
    Animator animator;
    bool canShoot = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void IsMoving(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void IsJumping(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);
    }

    public void IsFalling(bool isFalling)
    {
        animator.SetBool("isFalling", isFalling);
    }

    public void Shooting()
    {
        animator.SetTrigger("shooting");
    }

    public void Dead()
    {
        animator.SetTrigger("isDead");
    }

    public void Hit()
    {
        animator.SetTrigger("isHit");
    }

    public void AllowShooting()
    {
        canShoot = true;
    }

    public void DoNotAllowShooting()
    {
        canShoot = false;
    }

    public bool CanShoot()
    {
        return canShoot;
    }

    public void GameOver()
    {
        PlayerUI playerUI = GetComponentInParent<PlayerUI>();
        if (playerUI != null)
        {
            playerUI.GameOverUI();
        }
    }

    public void StartGame()
    {
        PlayerMovement playerMovement = GetComponentInParent<PlayerMovement>();
        if (playerMovement != null) { playerMovement.StartMoving(); }

        Blaster playerBlaster = GetComponentInParent<Blaster>();
        if (playerBlaster != null) { playerBlaster.StopShooting(false); }
    }
}
