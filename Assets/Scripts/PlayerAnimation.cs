using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Outside Inputs")]
    [SerializeField] PlayerMovement movement;
    Animator animator;

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
}
