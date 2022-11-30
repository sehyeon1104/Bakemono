using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    private string password = "";
    //int[] passwords = new int[4];

    private void Start()
    {
        CreatePassword();
    }

    void CreatePassword()
    {
        int num = 0;
        string temp = "";
        for(int i = 0; i < 4; ++i)
        {
            num = Random.Range(0, 10);
            temp = num.ToString();
            password += temp;
            //passwords[i] = num;
        }

        Debug.Log("Password : " + password);
    }
}
