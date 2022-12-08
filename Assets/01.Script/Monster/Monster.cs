using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour, IHittable
{
    int exp;
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
        //대충 적한테 맞았을 때 
    }

    void Awake()
    {
       playerBase = new PlayerBase();

    }
    private void Start()
    {
        maxHp = playerBase.MaxHP;
        currentHp = maxHp;
        exp = playerBase.Exp;
    }
    void Update()
    {
        
        if(!isDie)
        {
            playerBase.Exp = exp;
            playerBase.Level= level;
            playerBase.HP = currentHp; 
        }
    }
    void LevelUp()
    {
        
    }
}
