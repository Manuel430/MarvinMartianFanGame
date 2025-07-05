using UnityEngine;
using UnityEngine.InputSystem;

public class Blaster : MonoBehaviour
{
    [Header("Outside Inputs")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject ammo;
    [SerializeField] PlayerAnimation playerAnim;
    [SerializeField] LayerMask groundLayer;
    PlayerControls playerControls;
    PlayerUI playerUI;
    
    #region Public
    public void StopShooting(bool isHit)
    {
        if (isHit)
        {
            playerControls.Player.Disable();
        }
        else
        {
            playerControls.Player.Enable();
        }
    }
    #endregion

    private void Awake()
    {
        playerControls = new PlayerControls();

        playerUI = GetComponent<PlayerUI>();

        playerControls.Player.Enable();

        playerControls.Player.Shoot.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (playerUI.IsPaused()) { return; }

        if (!playerAnim.CanShoot()) { return; }

        playerAnim.Shooting();
        if (InGround())
        {
            return;
        }
        Instantiate(ammo, firePoint.position, firePoint.rotation);
    }

    private bool InGround() { return Physics2D.OverlapCircle(firePoint.position, 0.5f, groundLayer); }
}
