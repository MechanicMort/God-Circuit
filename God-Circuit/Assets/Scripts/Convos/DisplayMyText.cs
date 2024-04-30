using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DisplayMyText : MonoBehaviour
{
    public int Choice;
    int iterator = 0;
    StringBuilder displayText = new StringBuilder();
    public TMP_Text buttonText;
    // Start is called before the first frame update
    public void DisplayTextOnUI(string textToDisplay)
    {
        displayText.Clear();
        iterator = 0;
        StartCoroutine(DisplayText(displayText,textToDisplay));
    }

    public void HideButton()
    {

    }

    public void InputChoice()
    {
        GetComponentInParent<ConvoLogic>().InputChoice(Choice);
      
    }

    private void Update()
    {
        buttonText.text = displayText.ToString();
    }
    private IEnumerator DisplayText(StringBuilder text, string wantedText)
    {
        text.Append(wantedText[iterator]);
        iterator++;
        yield return new WaitForSeconds(0.1f);
        if (iterator != wantedText.Length)
        {
            StartCoroutine(DisplayText(text, wantedText));
        }
    }


}
