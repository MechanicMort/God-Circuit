using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControlPanel : MonoBehaviour
{
    public GameObject myEleva;
    public float myFloor;
    private GameObject debugHolder;
    // Update is called once per frame
    private void Start()
    {
  
    }
    public void MoveToFloor(float floor)
    {
        myFloor = floor;
        myEleva.GetComponent<Elavator>().MoveToFloor(myFloor);
    }
}
