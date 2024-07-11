using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Train : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10;
    public Vector3 moveDirection;
    public Vector3[] stations = new Vector3[2];

    private bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //inplement this with stations
        //currentFloor = Mathf.Round(transform.position.y);
        //if (currentFloor == targetFloor)
        //{
        //    OpenDoor();
        //    if (player.transform.parent == this.transform)
        //    {
        //        player.transform.SetParent(null, true);
        //    }
      //  }
        if (canMove)
        {
            transform.Translate(transform.right * Time.deltaTime * speed);
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            other.transform.parent.SetParent(this.transform,true);
            //other.GetComponent<CharacterController>().enabled = false;
            print("Parent");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent.SetParent(null, true);
            //other.GetComponent<CharacterController>().enabled = false;
            print("Parent");
        }
    }



}
