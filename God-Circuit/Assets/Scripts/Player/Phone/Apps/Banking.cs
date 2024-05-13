using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banking : MonoBehaviour
{
    public float moneyInAccount;
    public float overDraft;
    public float interstPerDay;
    public TMP_Text moneyDisplay;


    public void AddMoney(float input)
    {
        moneyInAccount += input;
    }


    public void Update()
    {
        moneyDisplay.text = "£" + moneyInAccount / 100;
    }
    public string GetMoney()
    {
        return "£" + moneyInAccount/100;
    }
    
}
