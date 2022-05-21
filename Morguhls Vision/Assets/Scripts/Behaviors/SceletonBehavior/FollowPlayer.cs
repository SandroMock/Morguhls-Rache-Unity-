using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : StateMachineBehaviour
{
    Transform player;
    Transform enemy;
    Time_Stop ts;
    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = Gamemanager.GM.GetNearestEnemy(animator.transform.position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
        agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy == null)
        {
            if (!ts.IsStopped)
            {
                agent.isStopped = false;
                enemy = Gamemanager.GM.GetNearestEnemy(animator.transform.position);
                if(enemy == null)
                {
                    agent.SetDestination(player.position);
                    if(Vector3.Distance(animator.transform.position, player.position) < 3)
                    {
                        agent.isStopped = true;
                        animator.SetBool("FollowPlayer", false);

                    }
                }
            }
        }
        else
        {
            animator.SetBool("ChaseEnemy", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
