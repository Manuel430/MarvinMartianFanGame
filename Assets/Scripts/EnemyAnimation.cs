using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [Header("Outside Inputs")]
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void IsShooting(bool isShooting)
    {
        animator.SetBool("isShooting", isShooting);
    }
}
