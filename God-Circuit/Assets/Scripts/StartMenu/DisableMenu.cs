using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMenu : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject UIPanel;

    public void Toggle()
    {
        UIPanel.SetActive(!UIPanel.activeSelf);

    }
}
