using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase
{
    public PlayerBase() // �÷��̾� ���̽� ����� �ʿ��� �� ȣ��
    {
        CallPlayerInfo();
    }
    int level;
    int hp;
    int maxHp=100;
    public int Level
    {
        get => level;
        set => level = value;
    }
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
        maxHp = SaveManager.Instance.CurrentUser.maxHp;
        hp = maxHp;
    }

}