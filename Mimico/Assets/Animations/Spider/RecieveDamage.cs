using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveDamage : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("A_Recieve", 0);
    }
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("A_Recieve", 0);
    }
}
