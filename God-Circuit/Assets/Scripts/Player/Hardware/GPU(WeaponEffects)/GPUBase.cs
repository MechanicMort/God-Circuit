using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUBase : MonoBehaviour
{
    public float powerDraw;
    public GameObject projectile;
    public GameObject motherBoard;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void GPUSetUP()
    {
        motherBoard = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireWeapon(GameObject projectileFired)
    {
        motherBoard.GetComponent<MotherBoard>().DrainPower(powerDraw);
        motherBoard.GetComponent<MotherBoard>().FireWeapon(projectile);



    }
}
