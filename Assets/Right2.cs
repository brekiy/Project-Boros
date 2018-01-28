using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right2 : StateMachineBehaviour {

    float hurtTime = 0;
    float hurtDelay = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        hurtTime = 34;
        hurtDelay = 34;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        hurtDelay -= 60 * Time.deltaTime;
        hurtDelay = Mathf.Max(0, hurtDelay);
        if (hurtDelay <= 0)
        {
            hurtTime -= 60 * Time.deltaTime;
            hurtTime = Mathf.Max(0, hurtTime);
        }

        if (hurtTime > 0 && hurtDelay <= 0)
        {
            animator.GetComponentInChildren<TreeCollision>().Damage(15f);
        }
        else
        {
            animator.GetComponentInChildren<TreeCollision>().Damage(0);
        }

    }
}
