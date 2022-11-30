using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public GameObject myEleva;
    public float myFloor;
    public GameObject DEBUGCUBE;
    private GameObject debugHolder;
    // Update is called once per frame
    private void Start()
    {
         debugHolder = Instantiate(DEBUGCUBE);
    }
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100))
            {

                print(hit.transform.name);
                debugHolder.transform.position = hit.point;
                if (hit.collider == GetComponent<Collider>())
                {
                    myEleva.GetComponent<Elavator>().MoveToFloor(myFloor);

                }
            }
              
        }     
    }
}
