using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCircuits : MonoBehaviour
{
    public GameObject canvas;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<BaseOverWorldController>().enabled = false;
            collision.transform.GetComponent<PlayerController>().enabled = true;
            canvas.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}
