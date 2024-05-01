using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class Inventory : MonoBehaviour
{
    public Sprite defaultSprite;
    public GameObject playerInvHolder;
    public List<GameObject> inventory = new List<GameObject>();
    public List<Sprite> inventoryImages = new List<Sprite>();
    public List<Image> imageUIElements = new List<Image>();
    public Transform dropLocation;
    private int inventorySize = 6;
    // Start is called before the first frame update
    void Start()
    {
        playerInvHolder = GameObject.FindGameObjectWithTag("InvHolder");
    }

    public void AddItem( GameObject newItem,Sprite image)
    {
        print("adding item");
        if (inventory.Count < inventorySize)
        {
            inventory.Add(newItem);
            inventoryImages.Add(image);
              
            newItem.transform.SetParent(playerInvHolder.transform);
            newItem.SetActive(false);
            UpdateScreen();
            print("Added Item");
            return;
        }
        else
        {
            print("Inventory Full");
        }
  
    }

    public void DropItem(GameObject button)
    {
        if (button.GetComponentInParent<Pointer>().pointToObject == null)
        {
            print("NoObject");
           
        }
        else
        {
            GameObject manipObject = button.GetComponentInParent<Pointer>().pointToObject;
            print("Dropping" + manipObject.name);
            inventory.Remove(manipObject);
            inventoryImages.Remove(manipObject.GetComponent<ItemHolder>().item.itemImage);
            manipObject.transform.SetParent(null);
            manipObject.transform.position = dropLocation.position;
            manipObject.SetActive(true);
            UpdateScreen();


        }
      
    }

    public void UpdateScreen()
    {
        for (int i = 0; i < imageUIElements.Count; i++)
        {
            if (i >= inventory.Count)
            {
                imageUIElements[i].sprite = defaultSprite;
                imageUIElements[i].transform.GetComponent<Pointer>().pointToObject = null;
            }
            else
            {
                if (inventory[i] != null)
                {
                    imageUIElements[i].sprite = inventoryImages[i];
                    imageUIElements[i].transform.GetComponent<Pointer>().pointToObject = inventory[i];
                }
                else
                {
                    imageUIElements[i].sprite = defaultSprite;
                    imageUIElements[i].transform.GetComponent<Pointer>().pointToObject = null;
                }
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
