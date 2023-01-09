using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToSceneOnCollision : MonoBehaviour
{
    public int scene1 ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(scene1));
        }
    }

}
