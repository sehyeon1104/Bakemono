using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Mob_Doctor : AI_Mob_Default
{
    
    public override int CurrentHp { get => currentHp; set => currentHp =value; }
    public override int MaxHp { get; set; }
    public override float Speed { get; set; }

    public override void Action(Transform target)
    {
        agent.isStopped = true;
        agent.SetDestination(target.position);
    }

    public override void GetHit(int damage, GameObject damgeDelear)
    {
        
    }

    public override void Idle()
    {

    }
    private void Start()
    {
     
    }
    public override void Move(Vector3 targetPos)
    {
        agent.isStopped = false;
        agent.SetDestination(targetPos);
    }
}
