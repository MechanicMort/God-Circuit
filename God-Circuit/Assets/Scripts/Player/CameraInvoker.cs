using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInvoker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.F))
        {

            print("Casting");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                print(hit.collider.name);
                if (hit.collider.GetComponent<InvokeInteraction>())
                {
                    hit.collider.GetComponent<InvokeInteraction>().InvokeTheInteraction();
                    print("Starting Invoke Chain");

                }
            }

        }
    }
    
}
