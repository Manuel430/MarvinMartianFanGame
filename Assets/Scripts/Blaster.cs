using UnityEngine;
using UnityEngine.InputSystem;

public class Blaster : MonoBehaviour
{
    [Header("Outside Inputs")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject ammo;
    [SerializeField] PlayerAnimation playerAnim;
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
        Instantiate(ammo, firePoint.position, firePoint.rotation);
    }
}
