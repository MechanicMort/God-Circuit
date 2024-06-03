using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleHook : MonoBehaviour
{
    private Rigidbody rb;
    public LineRenderer trail;
    private Vector3[] positions = new Vector3[2];
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        positions[0] = transform.position;
        positions[1] = GameObject.FindGameObjectWithTag("PoleTip").transform.position;
        trail.SetPositions( positions);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Fish")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PoleFishing>().Bite();
        }
    }

    public void FixedUpdate()
    {

        rb.AddForce(Physics.gravity);
    }
}
