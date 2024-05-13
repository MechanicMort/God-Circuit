using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public OverWorldItem item;
    public bool isWeapon;
    public bool canBePickedUp;
    public bool inBasket;
    public void PickupItem()
    {
        if (canBePickedUp)
        {
            print("Should Add Item");
            if (!isWeapon)
            {
                GameObject.FindGameObjectWithTag("Phone").GetComponent<Inventory>().AddItem(this.gameObject, item.itemImage);
            }
        }
 
       
    }

  public void ScanItem()
    {
        if (inBasket)
        {
            print("ItemDETECTEDINBASKET");
            GameObject.FindGameObjectWithTag("CheckOut").GetComponent<CheckOut>().ScanItem(this.gameObject);
        }

    }


}
