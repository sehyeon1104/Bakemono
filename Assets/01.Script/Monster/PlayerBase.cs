using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase
{
    public PlayerBase() // 플레이어 베이스 사용이 필요할 때 호출
    {
        CallPlayerInfo();
    }
    float levelPerExp = 0;
    float currentExp;
    int level;
    float hp;
    float maxHp = 100;
    public float LevelPerExp
    {
        get => levelPerExp;
        set => levelPerExp = value;
    }
    public float Exp
    {
        get => currentExp;
        set => currentExp = value;
    }
    public int Level
    {
        get => level;
        set => level = value;
    }
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if(hp < 0)
            {
                hp = 0;
            }
            else if(hp > maxHp)
            {
                hp = maxHp;
            }
        }
    }

    public float MaxHP { get { return maxHp; } }

    void CallPlayerInfo()
    {
        maxHp = Monster.Instance.MaxHp;
        hp = maxHp;
    }

}
