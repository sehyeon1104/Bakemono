using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mob_ZMonster : AI_Mob_Default
{
    readonly int hashHoriziontal = Animator.StringToHash("Horizontal");
    readonly int hashVertical = Animator.StringToHash("Vertical");
    readonly int hashLeftAttack = Animator.StringToHash("LeftAttack");
    readonly int hashRightAttack = Animator.StringToHash("RightAttack");
    readonly int hashBite = Animator.StringToHash("Bite");


    public override void Action(Transform target)
    {
        transform.LookAt(target.transform);
        if(actionCoroutine == null)
            actionCoroutine = StartCoroutine(Attack(5f));
    }

    public override void Idle()
    {
        if (actionCoroutine != null)
        {
            StopCoroutine("Attack");
            actionCoroutine = null;
        }
    }

    public override void Move(Vector3 targetPos)
    {
        agent.isStopped = false;

        if (actionCoroutine != null)
        {
            StopCoroutine("Attack");
            actionCoroutine = null;
        }

        anim.SetFloat(hashHoriziontal, agent.velocity.z);
        anim.SetFloat(hashVertical, agent.velocity.x);

        agent.SetDestination(targetPos);
    }

    private IEnumerator Attack(float attackDelay)
    {
        while(true)
        {
            anim.SetTrigger(hashLeftAttack);

            yield return new WaitForSeconds(0.3f);
            anim.SetTrigger(hashRightAttack);

            yield return new WaitForSeconds(1.5f);
            anim.SetTrigger(hashBite);

            yield return new WaitForSeconds(attackDelay);
        }
    }
}
