using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorDisplay : MonoBehaviour
{
    public TextMeshProUGUI textField;
    private Elavator Elavator;
    // Start is called before the first frame update
    void Start()
    {
        Elavator = GameObject.FindGameObjectWithTag("Elevator").GetComponent<Elavator>();
    }

    // Update is called once per frame
    void Update()
    {
        textField.text = Elavator.currentFloor.ToString();
    }
}
