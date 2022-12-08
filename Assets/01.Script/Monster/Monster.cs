using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour, IHittable
{
    int level = 0;
    bool isDie = false;
    public PlayerBase playerBase;
    int maxHp = 100;
    int currentHp = 100;
    [SerializeField] UnityEvent onDie;
    [SerializeField] UnityEvent levelUp;
    [SerializeField] UnityEvent onGethit;
    public void GetHit(int damage, GameObject damgeDelear)
    {
        //���� ������ �¾��� �� 
    }

    void Awake()
    {
       playerBase = new PlayerBase();
        maxHp = playerBase.MaxHP;
        currentHp = maxHp;
    }
    
    void Update()
    {
        if(isDie==false)
        {
            playerBase.Level= level;
            playerBase.HP = currentHp; 
        }
    }
}
