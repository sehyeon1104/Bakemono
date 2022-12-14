using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AI_Mob_Nurse : AI_Mob_Default
{
    Coroutine motionCoroutine = null;
    private void Update()
    {
    }
    public override int CurrentHp { get => base.CurrentHp; set => base.CurrentHp = value; }
    public override int MaxHp { get => base.MaxHp; set => base.MaxHp = value; }
    public override float Speed { get => base.Speed; set => base.Speed = value; }

    public override void Action(Transform target)
    {
        if (motionCoroutine != null)
            motionCoroutine = null;
        agent.isStopped = true;
        anim.SetBool(hashMove, false);
    }

    public override void GetHit(int damage, GameObject damgeDelear)
    {
        
    }

    public override void Idle()
    {
        anim.SetBool(hashMove, false);
        if (motionCoroutine == null)
            motionCoroutine = StartCoroutine(Motion());
    }

    public override void Move(Vector3 targetPos)
    {
        if (motionCoroutine != null)
            motionCoroutine = null;

        agent.isStopped = false;
        anim.SetBool(hashMove, true);
        agent.SetDestination(transform.position-targetPos);
    }

    private IEnumerator Motion()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            anim.SetTrigger(hashTrigger);
        }
    }

}
