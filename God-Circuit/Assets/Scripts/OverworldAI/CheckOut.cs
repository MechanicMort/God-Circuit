using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    public GameObject[] myLine;
    public GameObject BaggingArea;
    public GameObject BacketArea;
    public List<GameObject> items = new List<GameObject>();
    public float total;
    public float moneyGiven;
    public float changeNeeded;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void UnLoadCart(GameObject basket)
    {
        basket.transform.SetParent(BacketArea.transform);
        BacketArea.GetComponent<Collider>().enabled = false;
        basket.transform.localPosition = new Vector3(0, 0, 0);
        GameObject[] items = new GameObject[basket.transform.childCount];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = basket.transform.GetChild(i).gameObject;
            if (i!=0)
            {
                items[i].GetComponent<ItemHolder>().inBasket = true;
            }
         
        }

    }

    public void ScanItem(GameObject item)
    {
        total += item.GetComponent<ItemHolder>().item.itemCost;
        item.transform.position = BaggingArea.transform.position;
    }

    public void PayForItems(float MoneyGiven)
    {
        moneyGiven = MoneyGiven;
    }

    public Transform GetLinePos()
    {
        //should return last empty if no empty then return wander
        return myLine[0].transform;
    }
}
