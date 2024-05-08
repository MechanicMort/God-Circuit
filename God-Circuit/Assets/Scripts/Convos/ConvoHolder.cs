using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoHolder : MonoBehaviour
{
   public ConvoSO convoSO;
   public GameObject camSpot;
    public GameObject shopInterface;

    public void StartConvo()
    {
        print("Starting convo");
        GameObject.FindGameObjectWithTag("ConvoPanel").GetComponent<ConvoLogic>().ConvoToggle(convoSO, camSpot);
    }
}
