using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMenu : MonoBehaviour
{
    bool isOpen = true;
    // Start is called before the first frame update
    public GameObject UIPanel;



    private void Update()
    {
        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
        }
  
    }
    public void Toggle()
    {

        isOpen = !isOpen;
        Cursor.lockState = CursorLockMode.Locked;
        UIPanel.SetActive(!UIPanel.activeSelf);

    }
}
