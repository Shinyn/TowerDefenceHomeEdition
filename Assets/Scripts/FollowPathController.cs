using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathController : StateMachineBehaviour
{
    public GameObject pathPointsPrefab;
    Transform pathPoints;
    int nextPathPoint;
    public float moveSpeed = 5f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (pathPoints == null)
        {
            pathPoints = Instantiate(pathPointsPrefab).transform;
        }
        nextPathPoint = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 target = pathPoints.GetChild(nextPathPoint).position;
        if (Vector2.Distance(current, target) > 0.1)
        {
            animator.transform.position = Vector2.MoveTowards(current, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (nextPathPoint < 15)
            nextPathPoint++;
        }        
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
