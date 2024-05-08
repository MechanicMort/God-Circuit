using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ConvoSO", order = 2)]

public class ConvoSO : ScriptableObject
{
    public bool isShopKeeper;
   
    public string mySentance;
    public string myResponce;

    public List<ConvoSO> sentances = new List<ConvoSO>();

    
}
