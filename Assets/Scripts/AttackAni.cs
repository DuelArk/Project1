using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAni : StateMachineBehaviour
{
    [SerializeField] private int num;
    private Main_Player player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.gameObject.GetComponent<Main_Player>();
        player.attackCheck = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("nextAttack", true);
        }
        switch (num)
        {
            case 1:animator.gameObject.transform.position += Vector3.right * animator.gameObject.transform.localScale.x * Time.deltaTime * 2;
                break;
            case 2:animator.gameObject.transform.position += new Vector3(animator.gameObject.transform.localScale.x, 1, 0) * Time.deltaTime * 2;
                break;
            case 3:animator.gameObject.transform.position += new Vector3(animator.gameObject.transform.localScale.x, -1, 0) * Time.deltaTime * 2;
                break;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("nextAttack", false);
        animator.SetBool("AttackCheck", false);
        player.attackCheck = false;
        player.floorRaycastCheck();
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
