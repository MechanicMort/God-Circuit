using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[6];
    public Sprite[] inventoryImages = new Sprite[6];
    public Image[] imageUIElements = new Image[6];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddItem( GameObject newItem,Sprite image)
    {
        print("adding item");
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = newItem;
                inventoryImages[i] = image;
                newItem.SetActive(false);
                UpdateScreen();
                print("Added Item");
                return;
            }
        }
        print("Inventory Full");
    }

    public void UpdateScreen()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                imageUIElements[i].sprite = inventoryImages[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
