using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Mob_Doctor : AI_Mob_Default
{
    public override void Action(Transform target)
    {
        agent.isStopped = true;
        agent.SetDestination(target.position);
    }

    public override void Idle()
    {

    }

    public override void Move(Vector3 targetPos)
    {
        agent.isStopped = false;
        agent.SetDestination(targetPos);
    }
}
