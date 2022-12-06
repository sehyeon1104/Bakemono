using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI_Mob_Default : MonoBehaviour
{
    [Header("Action을 취하는 거리")]
    [SerializeField] private float actionDistance;
    [Header("Player 인식 거리")]
    [SerializeField] private float scanDistance;

    protected NavMeshAgent agent;
    protected Animator anim;

    public abstract void Action(Transform target);
    public abstract void Move(Vector3 targetPos);

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        DistanceCheck();
    }

    public void DistanceCheck()
    {
        if (MainModule.player == null) return;

        Transform playerPos = MainModule.player.transform;

        switch (Vector3.Distance(transform.position, playerPos.position))
        {
            case var a when a <= actionDistance:
                Action(playerPos);
                break;

            case var a when a > actionDistance && a <= scanDistance:
                Move(playerPos.position);
                break;

            default:
                break;
        }

    }
}
