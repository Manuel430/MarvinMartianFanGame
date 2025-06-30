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

    #region Cutscene
    public bool SetCutscene(bool inCutscene)
    {
        if (inCutscene)
        {
            playerControls.Player.Disable();
        }
        else
        {
            playerControls.Player.Enable();
        }

        return inCutscene;
    }
    #endregion

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        playerControls.Player.Shoot.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        playerAnim.Shooting();
        if (InGround())
        {
            return;
        }
        Instantiate(ammo, firePoint.position, firePoint.rotation);
    }

    private bool InGround() { return Physics2D.OverlapCircle(firePoint.position, 0.5f, groundLayer); }
}
