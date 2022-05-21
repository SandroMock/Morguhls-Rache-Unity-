using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BanditChase : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform skeleton;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        skeleton = Gamemanager.GM.GetNearestSceleton(animator.transform.position);
        agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(skeleton == null)
        {
            Gamemanager.GM.GetNearestSceleton(animator.transform.position);
            Debug.Log(skeleton);
            if (skeleton == null)
            {
                animator.SetBool("IsBack", true);
            }
        }
        else
        {
            agent.SetDestination(skeleton.position);
            skeleton = Gamemanager.GM.GetNearestSceleton(animator.transform.position);
            if(Vector3.Distance(animator.transform.position, skeleton.position) <= 2)
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
