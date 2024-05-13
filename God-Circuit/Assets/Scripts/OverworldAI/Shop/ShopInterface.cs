using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInterface : MonoBehaviour
{
    public GameObject playerPhone;
    private Button currentButt;


    public void Start()
    {
        playerPhone = GameObject.FindWithTag("Phone");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }

    public void SetItemNotOnSale(Image notsale)
    {
        notsale.gameObject.SetActive(true);
    }
    public void GiveButton(Button button)
    {
        currentButt = button;
    }
    public void BuyItem(ItemHolder itemHolder)
    {
        currentButt.interactable = false;

        if (playerPhone.GetComponent<Banking>().moneyInAccount > itemHolder.item.itemCost)
        {
            itemHolder.canBePickedUp = true;
            playerPhone.GetComponent<Inventory>().AddItem(itemHolder.transform.gameObject,itemHolder.item.itemImage);
        }
    }
}
