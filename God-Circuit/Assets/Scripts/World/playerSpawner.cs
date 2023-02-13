using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    GameObject player =   GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
    }

}
