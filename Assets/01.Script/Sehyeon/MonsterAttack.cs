using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    Animator monsterAni;
    readonly int attack = Animator.StringToHash("Attack");
    readonly int clickMore = Animator.StringToHash("ClickMore");
    void Start()
    {
        monsterAni= GetComponent<Animator>();
    }

    // Update is called once per frame
   public void Attack()
    {
        
    }
}
