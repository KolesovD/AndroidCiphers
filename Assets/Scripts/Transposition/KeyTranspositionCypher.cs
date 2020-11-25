using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyTranspositionCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField keyField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        if (!ValidateKey(out int keyArrayLength, out int[] keyIntArray))
            return;

        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;
        int numberOfRows = textLength % keyArrayLength == 0 ? textLength / keyArrayLength : textLength / keyArrayLength + 1;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < keyArrayLength; j++)
            {
                if ((i * keyArrayLength + keyIntArray[j]) >= textLength)
                    sb.Append(' ');
                else sb.Append(textToEncript[i * keyArrayLength + keyIntArray[j]]);
            }
        }

        outputField.text = sb.ToString().Trim();
    }

    public void Decript()
    {
        if (!ValidateKey(out int keyArrayLength, out int[] keyIntArray))
            return;

        int[] reverseKeyIntArray = new int[keyIntArray.Length];
        for (int i = 0; i < keyIntArray.Length; i++)
            reverseKeyIntArray[keyIntArray[i]] = i;

        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;
        int numberOfSplits = textLength % keyArrayLength == 0 ? textLength / keyArrayLength : textLength / keyArrayLength + 1;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberOfSplits; i++)
        {
            for (int j = 0; j < keyArrayLength; j++)
            {
                if ((i * keyArrayLength + reverseKeyIntArray[j]) >= textLength)
                    sb.Append(' ');
                else sb.Append(textToEncript[i * keyArrayLength + reverseKeyIntArray[j]]);
            }
        }

        outputField.text = sb.ToString().Trim();
    }

    private bool ValidateKey(out int keyArrLength, out int[] keyArr)
    {
        keyArrLength = 0;
        keyArr = new int[0];

        string key = keyField.text;

        if (key.Length == 0)
        {
            outputField.text = "Введите ключ";
            return false;
        }

        bool isValidate;
        if (char.IsDigit(key[0]))
            isValidate = ValidateDigitalKey(out keyArrLength, out keyArr);
        else isValidate = ValidateWordKey(out keyArrLength, out keyArr);

        return isValidate;
    }

    private bool ValidateDigitalKey(out int keyArrLength, out int[] keyArr)
    {
        keyArrLength = 0;
        keyArr = new int[0];

        string key = keyField.text;

        string[] keyArray = key.Split(new char[] { ' ', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        int keyArrayLength = keyArray.Length;
        int[] keyExistenceIntArray = new int[keyArrayLength];
        int[] keyIntArray = new int[keyArrayLength];

        for (int i = 0; i < keyArrayLength; i++)
        {
            int tempKey;
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

    private bool ValidateWordKey(out int keyArrLength, out int[] keyArr)
    {
        keyArrLength = 0;
        keyArr = new int[0];

        string key = keyField.text.ToLower();
        int keyLength = key.Length;

        int[] subKeyArray = new int[keyLength];
        for (int i = 0; i < keyLength; i++)
        {
            subKeyArray[i] = key[i];
            for (int j = 0; j < i; j++)
                if (subKeyArray[j] == subKeyArray[i])
                {
                    outputField.text = "Введён неверный ключ";
                    return false;
                }
        }

        int[] sortKeyArray = subKeyArray.Clone() as int[];
        Array.Sort(sortKeyArray);
        for (int i = 0; i < keyLength; i++)
        {
            for (int j = 0; j < keyLength; j++)
            {
                if (subKeyArray[i] == sortKeyArray[j])
                {
                    subKeyArray[i] = j;
                    break;
                }
            }
        }

        keyArrLength = keyLength;
        keyArr = subKeyArray;
        return true;
    }
}
