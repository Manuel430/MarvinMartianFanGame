using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [Header("Outside Inputs")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] PlayerAnimation playerAnim;
    Rigidbody2D rBody;
    PlayerControls playerControls;
    PlayerUI playerUI;

    [Header("Player Stats")]
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpPower;
    float horizontal;
    bool isFacingRight = true;
    bool isDead = false;

    [Header("Knockback")]
    [SerializeField] float kbForce;
    [SerializeField] float kbCounter;
    [SerializeField] float kbTotalTime;
    [SerializeField] bool knockFromRight;

    #region Public
    public void GameOver()
    {
        Debug.Log("Game Over");
        horizontal = 0;
        playerControls.Player.Disable();
        isDead = true;

        rBody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerAnim.Dead();
    }

    public void SetKBTimer()
    {
        playerAnim.Hit();
        kbCounter = kbTotalTime;
    }

    public bool GetKnockbackPush(bool isHitRight)
    {
        return knockFromRight = isHitRight;
    }

    public void StartMoving()
    {
        playerControls.Player.Enable();
    }

    public void StopMoving()
    {
        horizontal = 0;
        playerControls.Player.Disable();

        rBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    #endregion

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();

        playerControls = new PlayerControls();

        playerUI = GetComponent<PlayerUI>();

        playerControls.Player.Move.performed += Move;
        playerControls.Player.Move.canceled += Move;

        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Jump.canceled += Jump;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (IsGrounded())
        {
            playerAnim.IsFalling(false);
            playerAnim.IsJumping(false);
        }
        else
        {
            if (rBody.linearVelocityY < 0)
            {
                playerAnim.IsFalling(true);
                playerAnim.IsJumping(false);
            }
            else
                playerAnim.IsJumping(true);
        }
    }

    private void FixedUpdate()
    {
        if(isDead) { return; }

        if (kbCounter <= 0)
        {
            rBody.linearVelocity = new Vector2(horizontal * playerSpeed, rBody.linearVelocity.y);

            kbCounter = 0;
        }
        else
        {
            if (knockFromRight)
            {
                rBody.linearVelocity = new Vector2(-kbForce, kbForce);
            }
            else if (!knockFromRight)
            {
                rBody.linearVelocity = new Vector2(kbForce, kbForce);
            }

            kbCounter -= Time.deltaTime;
        }
    }

    private void Flip() { isFacingRight = !isFacingRight; transform.Rotate(0f, 180f, 0f); }
    private bool IsGrounded() { return Physics2D.OverlapCircle(groundCheck.position, 0.6f, groundLayer); }

    #region Inputs
    private void Move(InputAction.CallbackContext context)
    {
        if(playerUI.IsPaused()) { return; }

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
        if (playerUI.IsPaused()) { return; }

        if (context.performed && IsGrounded())
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
