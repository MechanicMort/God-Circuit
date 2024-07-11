using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public bool inBed = false;
    private GameObject player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.SetParent(null, true);
        player.GetComponentInChildren<CharacterController>().transform.position = transform.position;
     
        if (inBed )
        {
            StartCoroutine(BedStart());
        }
    }

    private IEnumerator BedStart() {
        yield return new WaitForEndOfFrame();

        player.transform.position = transform.position;
    }

}
