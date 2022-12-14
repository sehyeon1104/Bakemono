using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentStat
{
    int CurrentHp { get;  set; }
    int MaxHp { get;  set; }
    float Speed { get; set; }

}
