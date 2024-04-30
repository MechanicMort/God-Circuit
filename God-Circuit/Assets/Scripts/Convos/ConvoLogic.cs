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

    public GameObject choiceOne;
    public GameObject choiceTwo;
    public GameObject choiceThree;
    public TMP_Text convoText;
    StringBuilder displayText = new StringBuilder();
    StringBuilder choiceOneDisplayText = new StringBuilder();
    StringBuilder choiceTwoDisplayText = new StringBuilder();
    StringBuilder choiceThreeDisplayText = new StringBuilder();


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
            StartConvo(convoSO, newcCamSpot);
        }
    }

    
    public void StartConvo(ConvoSO convoSO,GameObject newCamSpot)
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
        StartCoroutine(DisplayText(displayText, placeInConvo.mySentance));
        DisplayChoices();
    }

    public void InputChoice(int num)
    {
        print(num);

    }

    public void DisplayChoices()
    {
        choiceOne.GetComponentInChildren<DisplayMyText>().DisplayTextOnUI(placeInConvo.sentances[0].mySentance);
        choiceTwo.GetComponentInChildren<DisplayMyText>().DisplayTextOnUI(placeInConvo.sentances[1].mySentance);
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

    }
}
