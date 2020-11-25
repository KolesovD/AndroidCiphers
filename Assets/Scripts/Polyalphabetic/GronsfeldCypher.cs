using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GronsfeldCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField keyField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        if (keyField.text.Length != 0)
        {
            List<int> key = ValidateKey(keyField.text);
            if (key != null)
                DoCesar(key, true);
        }
        else outputField.text = inputField.text;
    }

    public void Decript()
    {
        if (keyField.text.Length != 0)
        {
            List<int> key = ValidateKey(keyField.text);
            if (key != null)
                DoCesar(key, false);
        }
        else outputField.text = inputField.text;
    }

    private void DoCesar(List<int> key, bool encript)
    {
        string textToEncript = inputField.text;
        if (textToEncript.Length > 0)
        {
            CharEnumerator inCE = textToEncript.GetEnumerator();
            StringBuilder sb = new StringBuilder();

            List<List<char>> listOfAlphabets = new List<List<char>>();
            listOfAlphabets.Add(new List<char>(Alphabets.russian));
            listOfAlphabets.Add(new List<char>(Alphabets.russianBig));
            listOfAlphabets.Add(new List<char>(Alphabets.english));
            listOfAlphabets.Add(new List<char>(Alphabets.englishBig));

            int keyLength = key.Count;
            int currentKey = 0;

            char tempChar;
            while (inCE.MoveNext())
            {
                tempChar = inCE.Current;
                int tempShift;
                bool findList = false;
                foreach (List<char> alphabet in listOfAlphabets)
                {
                    if (alphabet.Contains(tempChar))
                    {
                        tempShift = encript ? key[currentKey] : -key[currentKey];
                        while (tempShift < 0)
                            tempShift += alphabet.Count;
                        tempShift = tempShift % alphabet.Count;
                        sb.Append(alphabet[(alphabet.IndexOf(tempChar) + tempShift) % alphabet.Count]);
                        findList = true;
                        if (++currentKey >= keyLength)
                            currentKey = 0;
                        break;
                    }
                }
                if (!findList)
                    sb.Append(tempChar);
            }
            outputField.text = sb.ToString();
        }
        else outputField.text = "";
    }

    private List<int> ValidateKey(string key)
    {
        List<int> intKey = new List<int>();
        for (int i = 0; i < key.Length; i++)
        {
            if (key[i] >= '0' && key[i] <= '9')
                intKey.Add(key[i] - '0');
            else return null;
        }
        return intKey;
    }
}
