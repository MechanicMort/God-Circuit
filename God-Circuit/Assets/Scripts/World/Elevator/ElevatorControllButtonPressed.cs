using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControllButtonPressed : MonoBehaviour
{
    public GameObject myControlPanel;
    public float myFloor;

    // Update is called once per frame
  
    public void MoveToFloor()
    {
        myControlPanel.GetComponent<ElevatorControlPanel>().MoveToFloor(myFloor);
    }
}
