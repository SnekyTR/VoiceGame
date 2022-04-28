using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Int : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entra1");
        animator.SetInteger("A_FireBall", 5400);
        animator.SetInteger("A_Movement", 5400);
        animator.SetInteger("A_FireBall", 5400);
    }

}
