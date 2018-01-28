using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour {
    protected Animator animator;

    public bool ikActive = false;
    public Transform target = null;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    void OnAnimatorIK()
    {
        if (animator)
        {
            if(target != null)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(target.position);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 5);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 5);
                animator.SetIKPosition(AvatarIKGoal.RightHand, target.position);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, target.position);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);
            }

        }
    }
}
