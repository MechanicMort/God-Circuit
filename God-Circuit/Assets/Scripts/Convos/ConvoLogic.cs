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
    StringBuilder displayText = new StringBuilder();
    string wantedText;
    int iterator =0;
    public TMP_Text convoText;
    private bool inConvo = false;

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
            StartConvo(convoSO);
        }
    }
    private void StartConvo(ConvoSO convoSO)
    {
        player.playerCameraParent.transform.position = camSpot.transform.position;
        player.playerCameraParent.transform.rotation = camSpot.transform.rotation;
        Cursor.lockState = CursorLockMode.Confined;
        player.canMove = false;
        displayText.Clear();
        convoPanel.SetActive(true);
        wantedText = convoSO.introSentance;
        iterator = 0;
        StartCoroutine(DisplayText());
    }

    public void InputChoice(int num)
    {

    }

    public void DisplayChoices()
    {

    }

    private void EndConvo()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.playerCameraParent.transform.localPosition = new Vector3(0, 0.721000016f, 0);
        player.canMove = true;
        convoPanel.SetActive(false);
    }

    private IEnumerator DisplayText()
    {
        displayText.Append(wantedText[iterator]);
        iterator++;
        yield return new WaitForSeconds(0.1f);
        if (iterator != wantedText.Length)
        {
            StartCoroutine(DisplayText());
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
