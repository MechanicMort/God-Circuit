using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
  public void OpenApp(GameObject appToOpen)
    {
        appToOpen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
