using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mob_Nurse : AI_Mob_Default
{
    public override void Action(Transform target)
    {
        agent.isStopped = true;
    }

    public override void Move(Vector3 targetPos)
    {
        agent.isStopped = false;
        agent.SetDestination(new Vector3(transform.position.x - targetPos.x , 0f, transform.position.z - targetPos.z));
    }
}
