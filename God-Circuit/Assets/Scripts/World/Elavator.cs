using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elavator : MonoBehaviour
{

    public GameObject player;
    public GameObject elevatorParent;
    public GameObject rightDoor;
    public GameObject leftDoor;
    public GameObject leftDoorOpenPosition;
    public GameObject leftDoorClosePosition;
    public GameObject rightDoorOpenPosition;
    public GameObject rightDoorClosedPosition;

    public float targetFloor;
    public float currentFloor;
    public float elevatorSpeed;

    // Start is called before the first frame update
    void Start()
    {
        elevatorParent = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            other.transform.SetParent(this.transform, true);
            //other.GetComponent<CharacterController>().enabled = false;
            print("Parent");
        }
    }

    public void OpenDoor()
    {
        rightDoor.transform.position = rightDoorOpenPosition.transform.position;
        leftDoor.transform.position = leftDoorOpenPosition.transform.position;
    }

    public void CloseDoor()
    {
        rightDoor.transform.position = rightDoorClosedPosition.transform.position;
        leftDoor.transform.position = leftDoorClosePosition.transform.position;
    }

    public void MoveElevator()
    {
        if (targetFloor > transform.position.y)
        {

            elevatorParent.transform.Translate(new Vector3(0,elevatorSpeed,0));
        }
        else
        {
            elevatorParent.transform.Translate(new Vector3(0, -elevatorSpeed, 0));
        }
    }

    public void MoveToFloor(float target)
    {    
        targetFloor = target;
        print("Moving To Floor" + targetFloor);
    }
        // Update is called once per frame
     
    void FixedUpdate()
    {
        currentFloor = Mathf.Round(transform.position.y);
        if (currentFloor == targetFloor)
        {
            OpenDoor();

        }
        else
        {
            CloseDoor();
            MoveElevator();
        }
    }
}
