using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    public WeatherController weatherController;
    public TMP_Text time;
    public void Update()
    {
        time.text = weatherController.GetTime();
    }
    public void OpenApp(GameObject appToOpen)
    {
        appToOpen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
