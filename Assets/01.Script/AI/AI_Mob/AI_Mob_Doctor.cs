using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mob_Doctor : AI_Mob_Default
{
    [SerializeField] private Transform punchTransform;
    public override void Action(Transform target)
    {
        agent.isStopped = true;

        anim.SetBool(hashMove, false);
        anim.SetBool(hashAttack, true);
        
        transform.LookAt(target.position);

        if(actionCoroutine == null)
            actionCoroutine = StartCoroutine(Attack(1f));
    }

    public override void Idle()
    {
        agent.isStopped = true;
        anim.SetBool(hashMove, false);
        anim.SetBool(hashAttack, false);

        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
            actionCoroutine = null;
        }
    }

    public override void Move(Vector3 targetPos)
    {
        agent.isStopped = false;

        anim.SetBool(hashAttack, false);
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
            Collider[] cols = Physics.OverlapSphere(punchTransform.position,1f);
            foreach(var col in cols)
            {
                if(col.CompareTag("Monster"))
                {
                    col.GetComponent<Monster>().GetHit(5f, gameObject);
                }
            }
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
