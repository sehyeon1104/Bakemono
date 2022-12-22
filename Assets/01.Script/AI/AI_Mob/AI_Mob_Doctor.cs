using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AI_Mob_Doctor : AI_Mob_Default
{
    [SerializeField] private Transform punchTransform;

    private IObjectPool<AI_Mob_Doctor> maleResearcherPool;
    public void SetPool(IObjectPool<AI_Mob_Doctor> pool)
    {
        EnemySpawnManager.Instance.maleResearcherPool = pool;
    }

    private void OnBecameInvisible()
    {
        EnemySpawnManager.Instance.maleResearcherPool.Release(this);
    }


    public override void Action(Transform target)
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        anim.SetBool(hashMove, false);
        anim.SetBool(hashAttack, true);
        
        transform.LookAt(target.position);

        if(actionCoroutine == null)
            actionCoroutine = StartCoroutine(Attack(1f));
    }

    public override void Idle()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

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

            yield return new WaitForSeconds(0.2f);
            
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
