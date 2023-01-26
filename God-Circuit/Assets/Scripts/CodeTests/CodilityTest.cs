using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodilityTest : MonoBehaviour
{

    public int[] A;
    public int N;

    private int numberFound;
    private int previousNumber;

    // Start is called before the first frame update
    void Start()
    {

        Solution(A);
    }

    public int Solution(int[] A)
    {
        List<int> list = A.ToList<int>();

        // Implement your solution here
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] < 0)
            {
                list.RemoveAt(i);
                i--;
            }
            //remove negative members
           
        }
        //if count = 0 then return 1
        //sort list
        list.Sort();
        if (list.Count > 0)
        {
            previousNumber = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (previousNumber + 1 != list[i])
                {
                    numberFound = previousNumber + 1;
                    print("Number Found: " + numberFound);
                    return numberFound;
                }
                else
                {
                    previousNumber = list[i];
                }
            }
            previousNumber += 1;
            print("Number Found: " + previousNumber);
            return previousNumber;
            for (int i = 0; i < list.Count; i++)
            {
                print(list[i]);
            }
        }
        else
        {
            print("Number Found: " + 1);
        }
        //else loop through find missing int of no missing then 

        return 1;
    }
}
