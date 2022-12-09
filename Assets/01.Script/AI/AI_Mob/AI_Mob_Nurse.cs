using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mob_Nurse : AI_Mob_Default
{
    private int hashMove = Animator.StringToHash("Move");

    public override void Action(Transform target)
    {
        agent.isStopped = true;
        anim.SetBool(hashMove, false);
    }

    public override void Idle()
    {
        anim.SetBool(hashMove, false);
    }

    public override void Move(Vector3 targetPos)
    {
        Debug.Log("µµ¸Á!");
        agent.isStopped = false;
        anim.SetBool(hashMove, true);
        agent.SetDestination(new Vector3(transform.position.x - targetPos.x , 0f, transform.position.z - targetPos.z));
    }
}
