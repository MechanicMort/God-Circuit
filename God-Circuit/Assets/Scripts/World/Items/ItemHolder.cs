using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public OverWorldItem item;
    public void PickupItem()
    {
        print("Should Add Item");
        GameObject.FindGameObjectWithTag("Phone").GetComponent<Inventory>().AddItem(this.gameObject,item.itemImage);
    }
}
