using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mob_Soldier : AI_Mob_Default
{

    public override void Action(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) < 3)
        {
            agent.isStopped = false;
            anim.SetBool(hashMove, true);
            anim.SetBool(hashAttack, false);
            agent.SetDestination(transform.position - target.position);

            if (actionCoroutine != null)
            {
                StopCoroutine(actionCoroutine);
                actionCoroutine = null;
            }
        }
        else
        {
            agent.isStopped = true;
            anim.SetBool(hashMove, false);
            anim.SetBool(hashAttack, true);

            transform.LookAt(target.position);

            if (actionCoroutine == null)
                actionCoroutine = StartCoroutine(Attack(2f));
        }

    }

    public override void Idle()
    {
        agent.isStopped = true;
        anim.SetBool(hashMove, false);

        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
            actionCoroutine = null;
        }
    }

    public override void Move(Vector3 targetPos)
    {
        agent.isStopped = false;
        anim.SetBool(hashMove, true);
        agent.SetDestination(targetPos);

        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
            actionCoroutine = null;
        }
    }

    private IEnumerator Attack(float attackDelay)
    {
        while (true)
        {
            anim.SetTrigger(hashTrigger);
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
