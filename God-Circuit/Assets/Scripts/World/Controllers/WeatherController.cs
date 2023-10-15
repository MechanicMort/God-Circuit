using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.Rendering;

public class WeatherController : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;
    public GameObject rain;
    public Volume skyandfog;
    public DateTime currentTime;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }
    public string GetTime()
    {
        return "00:00";
    }
    private void FixedUpdate()
    {
        sun.transform.Rotate(new Vector3(1, 0, 0), rotationSpeed);
        moon.transform.Rotate(new Vector3(1, 0, 0), rotationSpeed);
    }


}
