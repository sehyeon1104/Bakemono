using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentStat
{
    float CurrentHp { get;  set; }
    float MaxHp { get;  set; }
    float Speed { get; set; }

}
