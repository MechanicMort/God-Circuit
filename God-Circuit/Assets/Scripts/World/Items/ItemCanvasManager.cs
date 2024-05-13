using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCanvasManager : MonoBehaviour
{
    public OverWorldItem myItem;
    public ItemHolder itemHolder;
    public TMP_Text itemNameText;
    public TMP_Text itemCostText;
    public Image Image;
    public GameObject panel;
    public bool inInv;

    // Start is called before the first frame update
    void Start()
    {
        myItem = itemHolder.item;
        Image.sprite = myItem.itemImage;
        itemNameText.text = myItem.itemName;
        itemCostText.text = "£" + myItem.itemCost/100;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 4 || inInv)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }
}
