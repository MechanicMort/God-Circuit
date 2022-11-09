using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponentSwap : MonoBehaviour
{

    public GameObject component;
    public GameObject[] inventory;
    public UICOmponentManager manager;
    public GameObject previousSlot;


    public void SelectComponent()
    {

        manager = GameObject.FindGameObjectWithTag("MotherBoard").GetComponent<UICOmponentManager>();
        if (tag == "Component")
        {
            component = transform.parent.parent.parent.gameObject;
            manager.SelectObject(component);
        }
        if (component != null)
        {
            manager.selectedComponent = component;
        }

    }

    public void ClearThisSlot()
    {
        component = null;
    }

    public void MoveToThisSlot()
    {
        if (component == null)
        {
            if (manager.selectedComponent != null /* check in the right spot*/ )
            {
                print("swaping");
                component = manager.selectedComponent;
                component.transform.position = transform.position;
                if (component.GetComponentInChildren<UIComponentSwap>().previousSlot != null)
                {
                    component.GetComponentInChildren<UIComponentSwap>().previousSlot.GetComponentInChildren<UIComponentSwap>().ClearThisSlot();
                }
                manager.selectedComponent = null;
                manager.ComponentSwapped();

            }
        }
     
    }

    public void MoveToInventory()
    {
        print("adding Component To inventory:" + component.name);
        if (component != null)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    component = inventory[i];
                    component = null;
                    break;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("MotherBoard").GetComponent<MotherBoard>().Inventory;
        manager = GameObject.FindGameObjectWithTag("MotherBoard").GetComponent<UICOmponentManager>();

        if (tag == "Component")
        {
            component = transform.parent.parent.parent.gameObject;
        }
        else if (component != null)
        {
            component.transform.position = transform.position;
            component.GetComponentInChildren<UIComponentSwap>().previousSlot = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
