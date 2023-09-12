using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLocation : MonoBehaviour
{
    public string myFood;
    public List<GameObject> myShopItems;

    private void OnTriggerEnter(Collider other)
    {
        print("TiggerEntered");
        if (other.gameObject.GetComponent<Customer>())
        {
          
            other.gameObject.GetComponent<Customer>().PickUpItem(myShopItems[0]);
        }
    }
  

}
