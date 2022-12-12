using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManage : MonoBehaviour
{
    Monster monster;
    private void Awake()
    {
        monster = GameObject.FindObjectOfType<Monster>().GetComponent<Monster>();
    }
    public void LevelControler(int level)
    {
        switch(level)
        {
            case 1:
                monster.LevelPerExp = 10;
                break;

        }
        
    }
    public void LevelUp(int level)
    {
        level++;
        monster.CurrentExp -= monster.LevelPerExp;
    }
}
