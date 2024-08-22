using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRandomSpeed : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float randomTime = Random.Range(0f, 1f);
        animator.Play(stateInfo.fullPathHash, -1, randomTime);
    }
}
