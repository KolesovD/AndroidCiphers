using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RouteTranspositionCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    //[SerializeField] TMP_InputField keyField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;
        int oneSideDemention = (int)Mathf.Sqrt(textLength);
        if (oneSideDemention * oneSideDemention != textLength)
            oneSideDemention++;
        StringBuilder sb = new StringBuilder();
        for (int i = oneSideDemention - 1; i >= 0; i--)
        {
            for (int j = 0; j < oneSideDemention; j++)
            {
                if ((j * oneSideDemention + i) < textLength)
                    sb.Append(textToEncript[j * oneSideDemention + i]);
                else sb.Append(' ');
            }
        }

        outputField.text = sb.ToString();
    }

    public void Decript()
    {
        string textToDecript = inputField.text;
        int textLength = textToDecript.Length;
        int oneSideDemention = (int)Mathf.Sqrt(textLength);
        if (oneSideDemention * oneSideDemention != textLength)
            oneSideDemention++;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < oneSideDemention; i++)
        {
            for (int j = oneSideDemention - 1; j >= 0; j--)
            {
                if ((j * oneSideDemention + i) < textLength)
                    sb.Append(textToDecript[j * oneSideDemention + i]);
                else sb.Append(' ');
            }
        }

        outputField.text = sb.ToString().Trim();
    }
}
