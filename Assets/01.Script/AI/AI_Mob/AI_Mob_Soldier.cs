using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AI_Mob_Soldier : AI_Mob_Default
{
    [SerializeField] private Transform handPos;
    [SerializeField] private ParticleSystem muzzleFlash;

    private IObjectPool<AI_Mob_Soldier> soldierPool;
    public void SetPool(IObjectPool<AI_Mob_Soldier> pool)
    {
        EnemySpawnManager.Instance.soldierPool = pool;
    }

    private void OnBecameInvisible()
    {
        EnemySpawnManager.Instance.soldierPool.Release(this);
    }

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
                StopCoroutine("Attack");
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
        anim.SetBool(hashAttack, false);

        if (actionCoroutine != null)
        {
            StopCoroutine("Attack");
            actionCoroutine = null;
        }
    }

    public override void Move(Vector3 targetPos)
    {
        agent.isStopped = false;

        anim.SetBool(hashMove, true);
        anim.SetBool(hashAttack, false);

        agent.SetDestination(targetPos);

        if (actionCoroutine != null)
        {
            StopCoroutine("Attack");
            actionCoroutine = null;
        }
    }

    private IEnumerator Attack(float attackDelay)
    {
        while (true)
        {
            anim.SetTrigger(hashTrigger);
            var hits = Physics.SphereCastAll(handPos.position, 0.2f, transform.forward, 6f);
            foreach(var hit in hits)
            {
                if(hit.transform.CompareTag("Monster"))
                {
                    hit.transform.GetComponent<Monster>().GetHit(enemySO.Power, gameObject);
                }
            }

            muzzleFlash.Play();
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
