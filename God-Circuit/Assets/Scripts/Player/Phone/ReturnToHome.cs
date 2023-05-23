using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToHome : MonoBehaviour
{
    public GameObject  homeScreen;
    public void OpenApp(GameObject appToOpen)
    {
        homeScreen.SetActive(true);
        this.gameObject.SetActive(false);

    }
}
