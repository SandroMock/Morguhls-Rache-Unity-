using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardChase : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform sceleton;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        sceleton = Gamemanager.GM.GetNearestSceleton(animator.transform.position);
        agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (sceleton == null)
        {
            Gamemanager.GM.GetNearestSceleton(animator.transform.position);
            Debug.Log(sceleton);
            if (sceleton == null)
            {
                animator.SetBool("IsBack", true);
            }
        }
        else
        {
            agent.SetDestination(sceleton.position);
            sceleton = Gamemanager.GM.GetNearestSceleton(animator.transform.position);
            if (sceleton != null && Vector3.Distance(animator.transform.position, sceleton.position) <= 2)
            {
                agent.isStopped = true;
                animator.SetBool("IsCombat", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsChase", false);
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
