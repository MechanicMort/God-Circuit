using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;


public class BillboardCreator : MonoBehaviour
{
    public Sprite billboardSprites; 
    public BillboardAsset newAsset;
    // Start is called before the first frame update
    void Start()
    {
        newAsset = new BillboardAsset();
        AssetDatabase.CreateAsset(newAsset, "C:\\Users\\skuld\\Desktop\\God-Circuit\\God-Circuit\\God-Circuit\\Test\\.");
        AssetDatabase.SaveAssets();
        Instantiate(newAsset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
