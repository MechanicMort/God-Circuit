using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ConvoLogic : MonoBehaviour
{
   public GameObject convoPanel;
   public GameObject camSpot;
   public BaseOverWorldController player;
   int iterator =0;

    [Header("UIObjects")]
    public GameObject[] choiceUIObjects = new GameObject[3];
    public TMP_Text convoText;
    StringBuilder displayText = new StringBuilder();
    StringBuilder choiceOneDisplayText = new StringBuilder();
    StringBuilder choiceTwoDisplayText = new StringBuilder();
    StringBuilder choiceThreeDisplayText = new StringBuilder();

    public GameObject shopPanel;


    private bool inConvo = false;
    [Header("ConvoHolders")]
    private ConvoSO placeInConvo;
    public void ConvoToggle(ConvoSO convoSO,GameObject newcCamSpot)
    {
        camSpot = newcCamSpot;
        if (inConvo)
        {
            inConvo = !inConvo;
            EndConvo();
        }
        else
        {

            inConvo = !inConvo;
            StartConvo(convoSO, newcCamSpot,convoSO.isShopKeeper);
        }
    }

    
    public void StartConvo(ConvoSO convoSO,GameObject newCamSpot,bool shopPanel)
    {
        placeInConvo = convoSO; 
        camSpot = newCamSpot;
        player.playerCameraParent.transform.position = camSpot.transform.position;
        player.playerCameraParent.transform.rotation = camSpot.transform.rotation;
        Cursor.lockState = CursorLockMode.Confined;
        player.canMove = false;
        displayText.Clear();
        convoPanel.SetActive(true);
        iterator = 0;
        StartCoroutine(DisplayText(displayText, placeInConvo.myResponce));
        if (placeInConvo.isShopKeeper)
        {
            DisplayShop();
            convoPanel.SetActive(false);
        }
        else
        {
            DisplayChoices();

        }
    }


    public void DisplayShop()
    {
        GameObject[] keepers = GameObject.FindGameObjectsWithTag("ShopKeeper");
        GameObject wantedKeeper  = null;
        float dist = Mathf.Infinity;
        for (int i = 0; i < keepers.Length; i++)
        {
            if (Vector3.Distance(transform.position, keepers[i].transform.position) < dist)
            {
                dist = Vector3.Distance(transform.position, keepers[i].transform.position);
                wantedKeeper = keepers[i];
            }
        }
        if (wantedKeeper != null)
        {

            wantedKeeper.GetComponent<ConvoHolder>().shopInterface.SetActive(true);
        }
    }
        
    public void InputChoice(int num)
    {
        print(num);
        StartConvo(placeInConvo.sentances[num], camSpot, placeInConvo.sentances[num].isShopKeeper);
    }

    public void DisplayChoices()
    {
        for (int i =0; i <placeInConvo.sentances.Count;i++)
        {
            choiceUIObjects[i].GetComponentInChildren<DisplayMyText>().DisplayTextOnUI(placeInConvo.sentances[i].mySentance);
        }
    }

    private void EndConvo()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.playerCameraParent.transform.localPosition = new Vector3(0, 0.721000016f, 0);
        player.canMove = true;
        convoPanel.SetActive(false);
    }

    private IEnumerator DisplayText(StringBuilder text,string wantedText)
    {
        text.Append(wantedText[iterator]);
        iterator++;
        yield return new WaitForSeconds(0.1f);
        if (iterator != wantedText.Length)
        {
            StartCoroutine(DisplayText(text,wantedText));
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        convoText.text = displayText.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndConvo();
        }
    }
}
