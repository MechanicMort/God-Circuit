using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUExample : GPUBase
{
    // Start is called before the first frame update
    void Start()
    {
        GPUSetUP();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !GameObject.FindGameObjectWithTag("MotherBoard").GetComponent<MotherBoard>().inBuildMode)
        {
          motherBoard.GetComponent<MotherBoard>().FireWeapon();
        }
    }


}
