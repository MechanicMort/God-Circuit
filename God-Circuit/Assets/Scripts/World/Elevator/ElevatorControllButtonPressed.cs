using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControllButtonPressed : MonoBehaviour
{
    public GameObject myControlPanel;
    public float myFloor;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    myControlPanel.GetComponent<ElevatorControlPanel>().MoveToFloor(myFloor);

                }
            }

        }
    }
}
