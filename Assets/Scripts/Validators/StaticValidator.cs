using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class StaticValidator
{
    public static bool ValidateWDKey(TMP_InputField keyField, TMP_InputField outputField, out int keyArrLength, out int[] keyArr)
    {
        keyArrLength = 0;
        keyArr = new int[0];

        string key = keyField.text;

        if (key.Length == 0)
        {
            if (outputField != null) outputField.text = "Введите ключ";
            return false;
        }

        bool isValidate;
        if (char.IsDigit(key[0]))
            isValidate = ValidateDigitalKey(keyField, outputField, out keyArrLength, out keyArr);
        else isValidate = ValidateWordKey(keyField, outputField, out keyArrLength, out keyArr);

        return isValidate;
    }

    public static bool ValidateDigitalKey(TMP_InputField keyField, TMP_InputField outputField, out int keyArrLength, out int[] keyArr)
    {
        keyArrLength = 0;
        keyArr = new int[0];

        string key = keyField.text;

        string[] keyArray = key.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
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
                    if (outputField != null) outputField.text = "Введён неверный ключ";
                    return false;
                }
            }
            else
            {
                if (outputField != null) outputField.text = "Введён неверный ключ";
                return false;
            }
        }

        foreach (int keyExistence in keyExistenceIntArray)
            if (keyExistence == 0)
            {
                if (outputField != null) outputField.text = "Введён неверный ключ";
                return false;
            }

        keyArrLength = keyArrayLength;
        keyArr = keyIntArray;
        return true;
    }

    public static bool ValidateWordKey(TMP_InputField keyField, TMP_InputField outputField, out int keyArrLength, out int[] keyArr)
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
                    if (outputField != null) outputField.text = "Введён неверный ключ";
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
