using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "Walk";
    private const string IS_IDLE = "Idle";
    [SerializeField] private Player player;

    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        if (player.IsWalking())
            animator.Play(IS_WALKING);
        else
            animator.CrossFade(IS_IDLE, 0.1f);
    }
}
