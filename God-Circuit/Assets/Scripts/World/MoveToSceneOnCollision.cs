using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToSceneOnCollision : MonoBehaviour
{
    public string scene1 ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //will add loading and also load animaiton of door opening
            SceneManager.LoadScene(scene1,LoadSceneMode.Single);
        }
    }

}
