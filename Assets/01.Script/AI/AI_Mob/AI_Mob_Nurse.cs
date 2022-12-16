using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AI_Mob_Nurse : AI_Mob_Default
{
    public override void Action(Transform target)
    {
        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
            actionCoroutine = null;
        }
        agent.isStopped = true;
        anim.SetBool(hashMove, false);
    }

    public override void Idle()
    {
        anim.SetBool(hashMove, false);
        if (actionCoroutine == null)
            actionCoroutine = StartCoroutine(Motion());
    }

    public override void Move(Vector3 targetPos)
    {
        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
            actionCoroutine = null;
        }

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
