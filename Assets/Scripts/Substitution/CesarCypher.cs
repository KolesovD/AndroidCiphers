using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CesarCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField shiftField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        DoCesar(int.Parse(shiftField.text));
    }

    public void Decript()
    {
        DoCesar(-int.Parse(shiftField.text));
    }

    private void DoCesar(int shift)
    {
        string textToEncript = inputField.text;
        if (shift != 0)
        {
            if (textToEncript.Length > 0)
            {
                CharEnumerator inCE = textToEncript.GetEnumerator();
                StringBuilder sb = new StringBuilder();

                List<List<char>> listOfAlphabets = new List<List<char>>();
                listOfAlphabets.Add(new List<char>(Alphabets.russian));
                listOfAlphabets.Add(new List<char>(Alphabets.russianBig));
                listOfAlphabets.Add(new List<char>(Alphabets.english));
                listOfAlphabets.Add(new List<char>(Alphabets.englishBig));

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
                            tempShift = shift;
                            while (tempShift < 0)
                                tempShift += alphabet.Count;
                            tempShift = tempShift % alphabet.Count;
                            sb.Append(alphabet[(alphabet.IndexOf(tempChar) + tempShift) % alphabet.Count]);
                            findList = true;
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
        else outputField.text = textToEncript;
    }
}
