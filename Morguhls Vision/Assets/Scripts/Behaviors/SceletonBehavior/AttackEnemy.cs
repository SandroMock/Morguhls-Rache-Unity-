using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : StateMachineBehaviour
{
    Transform enemy;
    float damageTimer = 1.5f;
    Time_Stop ts;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = Gamemanager.GM.GetNearestEnemy(animator.transform.position);
        ts = FindObjectOfType<Time_Stop>().GetComponent<Time_Stop>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!ts.IsStopped)
        {
            damageTimer -= Time.deltaTime;
            if(damageTimer <= 0)
            {
                animator.transform.LookAt(enemy);
                enemy.GetComponent<Enemy_Behavior>().GetDamage(10);
                damageTimer = 1.5f;
            }
        if (enemy != null)
        {
            enemy = Gamemanager.GM.GetNearestEnemy(animator.transform.position);
            if(enemy != null)
            {
                if (Vector3.Distance(animator.transform.position, enemy.position) > 2)
                {
                    animator.SetBool("AttackEnemy", false);
                }
            }
        }
        if (enemy == null)
        {
            //animator.SetBool("EnemyNull", true);
            animator.Play("FollowPlayer");
        }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        damageTimer = 1.5f;
        animator.SetBool("AttackEnemy", false);
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
