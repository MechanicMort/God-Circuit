using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : BaseAI
{

    public GameObject shoppingCart;
    private float Kindness;
    private string Name;
    private string Description;
    private string[] itemsWanted = new string[1];
    private string[] items = new string[1];
    public List<Transform> foodLocations = new List<Transform>();
    private List<Transform> pathLocations = new List<Transform>();
    private int locationsCompleted = 0;
    // Start is called before the first frame update



    void Start()
    {
        items[0] = "SodyPop";
        itemsWanted[0] = "SodyPop";
        StartOperations();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void CreatePath()
    {
        foodLocations.Clear();
        GetFoodLocations();
        for (int i = 0; i < itemsWanted.Length; i++)
        {
          //  print("Here");
            for (int x = 0; x < foodLocations.Count; x++)
            {
                print("Here Again");
                if (foodLocations[x].GetComponent<FoodLocation>().myFood == itemsWanted[i])
                {
                    pathLocations.Add(foodLocations[x]); 
                    x = foodLocations.Count;
                }
            }
        }
        
    }

    public void PickUpItem(GameObject shopItem)
    {
        if (locationsCompleted != itemsWanted.Length)
        {
            if (shopItem.GetComponent<ItemHolder>().item.itemName == itemsWanted[locationsCompleted])
            {
                //add shopping cart
                shopItem.transform.SetParent(shoppingCart.transform);
                shopItem.transform.position = shoppingCart.transform.position;
            }
            locationsCompleted++;
        }


    }



   override public IEnumerator MoveAI()
    {
        //create path
        if (pathLocations.Count == 0)
        {
            CreatePath();
        }
        myAgent.destination = pathLocations[locationsCompleted].transform.position;
        yield return new WaitForSeconds(1);

    }


    private void GetFoodLocations()
    {
        for (int i = 0; i < localNodes.Count; i++)
        {
            // add check to see if node is free
            if (localNodes[i].GetComponent<FoodLocation>())
            {
                foodLocations.Add(localNodes[i]);
            }
        }
    }

}
