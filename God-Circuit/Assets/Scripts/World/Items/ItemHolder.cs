using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public OverWorldItem item;

    public bool inBasket;
    public void PickupItem()
    {
        print("Should Add Item");
        GameObject.FindGameObjectWithTag("Phone").GetComponent<Inventory>().AddItem(this.gameObject,item.itemImage);
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
