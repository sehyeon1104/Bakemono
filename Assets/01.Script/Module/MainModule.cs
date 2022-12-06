using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainModule : MonoBehaviour
{
    public bool isPlayer = false;
    public static MainModule player;

    private void Awake()
    {
        if (isPlayer)
            player = this;

    }
}
