using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{

    public bool isInHand;
    public Vector3 starPOS;
    public float x, y, z,speed;
    public Animator animator;

    public bool upDown;

    public ItemCanvasManager itemCanvasManager;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Awake()
    {
        starPOS = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        if (!isInHand)
        {
            if (Vector3.Distance(transform.position,new Vector3(starPOS.x,starPOS.y,starPOS.z) ) >= 0.7)
            {
               upDown = true;
            }
            else if (Vector3.Distance(transform.position, new Vector3(starPOS.x, starPOS.y, starPOS.z)) < 0.3)
            {
                
                    upDown = false;
                
            }
      

            if (upDown)
            {
                transform.position = new Vector3(starPOS.x, Mathf.Lerp(transform.position.y, starPOS.y, speed*Time.deltaTime), starPOS.z);
            }
            else {
                transform.position = new Vector3(starPOS.x, Mathf.Lerp(transform.position.y, starPOS.y + 1, speed * Time.deltaTime), starPOS.z);
            }
            
            transform.Rotate(new Vector3(x, y, z), speed);
        }

    }

    public void DropWeapon()
    {
        if (isInHand)
        {
            isInHand = false;
            itemCanvasManager.inInv = false;
            animator.GetComponent<Animator>().enabled = false; 
        }
    }

    public void InvokePickUp()
    {
       
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().WeaponEquip(this.gameObject)) 
        {
            itemCanvasManager.inInv = true;
            isInHand =true;
        } 
    
    }
}
