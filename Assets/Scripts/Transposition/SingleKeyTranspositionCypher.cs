using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleKeyTranspositionCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField keyField1;
    [SerializeField] TMP_InputField keyField2;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        if (!ValidateKey(out int keyArray1Length, out int[] keyIntArray1, keyField1.text))
            return;

        if (!ValidateKey(out int keyArray2Length, out int[] keyIntArray2, keyField2.text))
            return;

        if (keyArray1Length != keyArray2Length)
        {
            outputField.text = "Введён неправильный ключ";
            return;
        }

        int[] keyIntArray = new int[keyArray1Length];
        for (int i = 0; i < keyArray1Length; i++)
            keyIntArray[keyIntArray1[i]] = keyIntArray2[i];

        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;

        int numberOfSplits = textLength % keyArray1Length == 0 ? textLength / keyArray1Length : textLength / keyArray1Length + 1;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberOfSplits; i++)
        {
            for (int j = 0; j < keyArray1Length; j++)
            {
                if ((i * keyArray1Length + keyIntArray[j]) >= textLength)
                    sb.Append(' ');
                else sb.Append(textToEncript[i * keyArray1Length + keyIntArray[j]]);
            }
        }

        outputField.text = sb.ToString();
    }

    public void Decript()
    {
        if (!ValidateKey(out int keyArray1Length, out int[] keyIntArray1, keyField1.text))
            return;

        if (!ValidateKey(out int keyArray2Length, out int[] keyIntArray2, keyField2.text))
            return;

        if (keyArray1Length != keyArray2Length)
        {
            outputField.text = "Введён неправильный ключ";
            return;
        }

        int[] reverseKeyIntArray = new int[keyArray1Length];
        for (int i = 0; i < keyArray1Length; i++)
            reverseKeyIntArray[keyIntArray2[i]] = keyIntArray1[i];

        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;

        int numberOfSplits = textLength % keyArray1Length == 0 ? textLength / keyArray1Length : textLength / keyArray1Length + 1;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberOfSplits; i++)
        {
            for (int j = 0; j < keyArray1Length; j++)
            {
                if ((i * keyArray1Length + reverseKeyIntArray[j]) >= textLength)
                    sb.Append(' ');
                else sb.Append(textToEncript[i * keyArray1Length + reverseKeyIntArray[j]]);
            }
        }

        outputField.text = sb.ToString();
    }

    private bool ValidateKey(out int keyArrLength, out int[] keyArr, string keyText)
    {
        keyArrLength = 0;
        keyArr = new int[0];

        if (keyText.Length == 0)
        {
            outputField.text = "Введите ключ";
            return false;
        }

        string[] keyArray = keyText.Split(new char[] { ' ', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        int keyArrayLength = keyArray.Length;
        int[] keyExistenceIntArray = new int[keyArrayLength];
        int[] keyIntArray = new int[keyArrayLength];

        for (int i = 0; i < keyArrayLength; i++)
        {
            int tempKey = 0;
            if (int.TryParse(keyArray[i], out tempKey))
            {
                if (tempKey <= keyArrayLength && tempKey > 0)
                {
                    keyExistenceIntArray[tempKey - 1] = 1;
                    keyIntArray[i] = tempKey - 1;
                }
                else
                {
                    outputField.text = "Введён неверный ключ";
                    return false;
                }
            }
            else
            {
                outputField.text = "Введён неверный ключ";
                return false;
            }
        }

        foreach (int keyExistence in keyExistenceIntArray)
            if (keyExistence == 0)
            {
                outputField.text = "Введён неверный ключ";
                return false;
            }

        keyArrLength = keyArrayLength;
        keyArr = keyIntArray;
        return true;
    }
}
