using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    Animator monsterAni;
    readonly int leftAttack = Animator.StringToHash("LeftAttack");
    readonly int rightAttack = Animator.StringToHash("RightAttack");
    void Start()
    {
        monsterAni= GetComponent<Animator>();
    }

    // Update is called once per frame
   public void Attack(bool isLeft)
    {
      if(isLeft)
        {
            monsterAni.SetTrigger(leftAttack);
            
        }
      else if(!isLeft)
        {
            monsterAni.SetTrigger(rightAttack);
        }
    }
}
