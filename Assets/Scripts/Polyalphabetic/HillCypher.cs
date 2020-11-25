using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HillCypher : MonoBehaviour
{
    /*[SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField keyField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        if (keyField.text.Length != 0)
        {
            int[,] key = ValidateKey(keyField.text);
            if (key != null)
            {
                string textToEncript = inputField.text;
                int[] currentText = new int[3];
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
            else outputField.text = "Неправильный ключ";
        }
        else outputField.text = inputField.text;
    }*/

    public void Decript()
    {
        /*if (keyField.text.Length != 0)
        {
            List<int> key = ValidateKey(keyField.text);
            if (key != null)
                DoHill(key, false);
            else outputField.text = "Неправильный ключ";
        }
        else outputField.text = inputField.text;*/
    }

    private int[,] ValidateKey(string key)
    {
        key = string.Join("", key.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        if (key.Length == 9)
        {
            List<List<char>> listOfAlphabets = new List<List<char>>();
            listOfAlphabets.Add(new List<char>(Alphabets.russian));
            listOfAlphabets.Add(new List<char>(Alphabets.russianBig));
            listOfAlphabets.Add(new List<char>(Alphabets.english));
            listOfAlphabets.Add(new List<char>(Alphabets.englishBig));
            int[,] matrix = new int[3, 3];
            for (int i = 0; i < key.Length; i++)
            {
                bool find = false;
                foreach (List<char> alphabet in listOfAlphabets)
                {
                    if (alphabet.Contains(key[i]))
                    {
                        matrix[i / 3, i % 3] = alphabet.IndexOf(key[i]);
                        find = true;
                        break;
                    }
                }
                if (!find)
                    return null;
            }
            return matrix;
        }
        return null;
    }
}