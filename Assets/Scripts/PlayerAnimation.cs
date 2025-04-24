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

    public void SetCutscene()
    {
        movement.SetCutscene(false);
    }

    public void IsMoving(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }
}
