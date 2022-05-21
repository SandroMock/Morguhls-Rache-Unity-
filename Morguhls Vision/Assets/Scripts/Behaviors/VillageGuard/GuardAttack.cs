using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttack : StateMachineBehaviour
{
    float damageTimer = 1.5f;
    Transform sceleton;
    Time_Stop ts;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sceleton = Gamemanager.GM.GetNearestSceleton(animator.transform.position);
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(sceleton);
        if (!ts.IsStopped)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0 && sceleton != null)
            {
                animator.transform.LookAt(sceleton);
                if (sceleton.TryGetComponent(out PlayerHealth ph))
                {
                    ph.GetDamage(10);
                }
                else
                {
                    sceleton.GetComponent<SceletonBehavior>().GetDamage(10);
                }
                damageTimer = 1.5f;
            }
        }
        if (sceleton != null)
        {
            sceleton = Gamemanager.GM.GetNearestSceleton(animator.transform.position);
            if (sceleton != null && Vector3.Distance(animator.transform.position, sceleton.position) > 2)
            {
                animator.SetBool("IsCombat", false);
            }
        }
        if (sceleton == null)
        {
            sceleton = Gamemanager.GM.GetNearestSceleton(animator.transform.position);
            if (sceleton == null)
            {
                animator.SetBool("IsBack", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        damageTimer = 1.5f;
        animator.SetBool("IsCombat", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
