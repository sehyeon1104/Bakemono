using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public  class AI_Mob_Nurse : AI_Mob_Default
{

    private IObjectPool<AI_Mob_Nurse> femaleResearcherPool;
    public void SetPool(IObjectPool<AI_Mob_Nurse> pool)
    {
        EnemySpawnManager.Instance.femaleResearcherPool = pool;
    }

    private void OnBecameInvisible()
    {
        EnemySpawnManager.Instance.femaleResearcherPool.Release(this);
    }

    public override void Action(Transform target)
    {
        if (actionCoroutine != null)
        {
            StopCoroutine("Motion");
            actionCoroutine = null;
        }
        agent.isStopped = true;
        anim.SetBool(hashMove, false);
    }

    public override void Idle()
    {
        agent.isStopped = true;
        agent.SetDestination(transform.position);

        anim.SetBool(hashMove, false);
        if (actionCoroutine == null)
            actionCoroutine = StartCoroutine(Motion());
    }

    public override void Move(Vector3 targetPos)
    {
        if (actionCoroutine != null)
        {
            StopCoroutine("Motion");
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
