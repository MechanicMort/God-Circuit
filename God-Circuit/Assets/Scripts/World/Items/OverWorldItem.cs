using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/OverWorldItem", order = 2)]
public class OverWorldItem : ScriptableObject
{
    // Start is called before the first frame update

    public Sprite itemImage;
    public GameObject myItem;
    
}
