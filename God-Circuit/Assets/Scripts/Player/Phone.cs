using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{

    public GameObject phoneScreen;
    public bool power;

    public MeshRenderer centreMeshRender;
    public Material on;
    public Material off;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (power == true)
        {
            phoneScreen.SetActive(true);
            centreMeshRender.material = on;

        }
        else
        {
            phoneScreen.SetActive(false);
            centreMeshRender.material = off;
        }
    }
}
