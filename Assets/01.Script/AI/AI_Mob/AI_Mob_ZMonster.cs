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

    [SerializeField] SphereCollider[] attackCol;


    public override void Action(Transform target)
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        transform.LookAt(target.transform);
        if(actionCoroutine == null)
            actionCoroutine = StartCoroutine(Attack(5f));
    }

    public override void Idle()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

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

    public void AttackTo(SphereCollider attackCol , float power)
    {
        Collider[] cols = Physics.OverlapSphere(attackCol.transform.position, attackCol.radius);
        foreach(var col in cols)
        {
            if(col.CompareTag("Monster"))
                col.gameObject.GetComponent<Monster>().GetHit(power, attackCol.gameObject);
        }
    }

    private IEnumerator Attack(float attackDelay)
    {
        while(true)
        {
            anim.SetTrigger(hashLeftAttack);
            yield return new WaitForSeconds(0.5f);
            AttackTo(attackCol[0], enemySO.Power);

            yield return new WaitForSeconds(0.5f);

            anim.SetTrigger(hashRightAttack);
            yield return new WaitForSeconds(0.6f);
            AttackTo(attackCol[1], enemySO.Power);

            yield return new WaitForSeconds(0.9f);
            anim.SetTrigger(hashBite);
            AttackTo(attackCol[2], enemySO.Power * 2);

            yield return new WaitForSeconds(attackDelay);
        }
    }

}
