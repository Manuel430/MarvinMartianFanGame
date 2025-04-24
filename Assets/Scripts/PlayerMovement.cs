using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Outside Inputs")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] PlayerAnimation playerAnim;
    Rigidbody2D rBody;
    PlayerControls playerControls;

    [Header("Player Stats")]
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpPower;
    float horizontal;
    bool isFacingRight = true;

    #region Cutscene
    public bool SetCutscene(bool cutscene)
    {
        if (cutscene)
        {
            horizontal = 0;
            playerControls.Player.Disable();
        }
        else
        {
            playerControls.Player.Enable();
        }

        return cutscene;
    }
    #endregion

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();

        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        playerControls.Player.Move.performed += Move;
        playerControls.Player.Move.canceled += Move;

        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Jump.canceled += Jump;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        rBody.linearVelocity = new Vector2(horizontal * playerSpeed, rBody.linearVelocity.y);
    }

    private void Flip() { isFacingRight = !isFacingRight; transform.Rotate(0f, 180f, 0f); }
    private bool IsGrounded() { return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); }

    #region Inputs
    private void Move(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            horizontal = context.ReadValue<Vector2>().x;
            playerAnim.IsMoving(true);
        }
        else if (context.canceled)
        {
            horizontal = 0f;
            playerAnim.IsMoving(false);
        }

        if(horizontal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0f && isFacingRight)
        {
            Flip();
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rBody.linearVelocity = new Vector2(rBody.linearVelocityX, jumpPower);
        }
        else if (context.canceled && rBody.linearVelocityY > 0)
        {
            rBody.linearVelocity = new Vector2(rBody.linearVelocityX, rBody.linearVelocityY * 0.5f);
        }
    }
    #endregion
}
