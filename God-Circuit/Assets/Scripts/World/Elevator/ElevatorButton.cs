using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public GameObject myEleva;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject leftDoorClosed;
    public GameObject rightDoorClosed;
    public GameObject leftDoorOpen;
    public GameObject rightDoorOpen;
    public float myFloor;
  //  public GameObject DEBUGCUBE;
    private GameObject debugHolder;
    // Update is called once per frame
    private void Start()
    {
       //  debugHolder = Instantiate(DEBUGCUBE);
    }

    public void Update()
    {
        if (myEleva.GetComponent<Elavator>().currentFloor == myFloor)
        {
            leftDoor.transform.position = leftDoorOpen.transform.position;
            rightDoor.transform.position = rightDoorOpen.transform.position;
        }
        else
        {
            leftDoor.transform.position = leftDoorClosed.transform.position;
            rightDoor.transform.position = rightDoorClosed.transform.position;
        }
    }

    public void ActivateButton()
    {
        myEleva.GetComponent<Elavator>().MoveToFloor(myFloor);
 
       
    }
}
