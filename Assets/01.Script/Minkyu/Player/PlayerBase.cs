using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase
{
    public PlayerBase() // 플레이어 베이스 사용이 필요할 때 호출
    {
        CallPlayerInfo();
    }

    int hp;
    int maxHp;

    public int HP
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

    public int MaxHP { get { return maxHp; } }

    void CallPlayerInfo()
    {
        maxHp = 10;
        hp = maxHp;
    }

}
