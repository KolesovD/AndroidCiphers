using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class TrycemusCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField keyField;
    [SerializeField] TMP_InputField outputField;

    private const int rowSize = 6;

    public void Encript()
    {
        string key = keyField.text;
        string textToEncript = inputField.text;
        if (key != null && key.Length != 0)
        {
            List<char> currentAlphabet;
            if (!ValidateKey(key, out currentAlphabet))
                outputField.text = "Неправильный ключ";
            else if (textToEncript.Length > 0)
            {
                CharEnumerator inCE = textToEncript.GetEnumerator();
                StringBuilder sb = new StringBuilder();

                List<char> transKey = new List<char>();

                foreach (char letter in key)
                    if (!transKey.Contains(char.ToLower(letter)) && letter != ' ')
                        transKey.Add(char.ToLower(letter));
                foreach (char letter in currentAlphabet)
                    if (!transKey.Contains(letter))
                        transKey.Add(letter);

                char tempChar;
                while (inCE.MoveNext())
                {
                    tempChar = inCE.Current;
                    if (transKey.Contains(char.ToLower(tempChar)))
                        sb.Append(char.IsLower(tempChar) ? transKey[(transKey.IndexOf(char.ToLower(tempChar)) + rowSize) % transKey.Count] : char.ToUpper(transKey[(transKey.IndexOf(char.ToLower(tempChar)) + rowSize) % transKey.Count]));
                    else sb.Append(tempChar);
                }
                outputField.text = sb.ToString();
            }
            else outputField.text = "";
        }
        else outputField.text = "Введите ключ";
    }

    public void Decript()
    {
        string key = keyField.text;
        string textToEncript = inputField.text;
        if (key != null && key.Length != 0)
        {
            List<char> currentAlphabet;
            if (!ValidateKey(key, out currentAlphabet))
                outputField.text = "Неправильный ключ";
            else if (textToEncript.Length > 0)
            {
                CharEnumerator inCE = textToEncript.GetEnumerator();
                StringBuilder sb = new StringBuilder();

                List<char> transKey = new List<char>();

                foreach (char letter in key)
                    if (!transKey.Contains(char.ToLower(letter)) && letter != ' ')
                        transKey.Add(char.ToLower(letter));
                foreach (char letter in currentAlphabet)
                    if (!transKey.Contains(letter))
                        transKey.Add(letter);

                char tempChar;
                while (inCE.MoveNext())
                {
                    tempChar = inCE.Current;
                    if (transKey.Contains(char.ToLower(tempChar)))
                        sb.Append(char.IsLower(tempChar) ? transKey[(transKey.IndexOf(char.ToLower(tempChar)) - rowSize + transKey.Count) % transKey.Count] : char.ToUpper(transKey[(transKey.IndexOf(char.ToLower(tempChar)) - rowSize + transKey.Count) % transKey.Count]));
                    else sb.Append(tempChar);
                }
                outputField.text = sb.ToString();
            }
            else outputField.text = "";
        }
        else outputField.text = "Введите ключ";
    }

    private bool ValidateKey(string key, out List<char> alphabet)
    {
        string loverKey = string.Join("", key.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        List<char> russian = new List<char>(Alphabets.russian);
        List<char> english = new List<char>(Alphabets.english);
        List<char> currentAlphabet;
        if (russian.Contains(loverKey[0]))
        {
            currentAlphabet = russian;
            alphabet = russian;
        }
        else if (english.Contains(loverKey[0]))
        {
            currentAlphabet = english;
            alphabet = english;
        }
        else
        {
            alphabet = null;
            return false;
        }

        for (int i = 0; i < loverKey.Length; i++)
        {
            if (!currentAlphabet.Contains(loverKey[i]))
                return false;
        }
        return true;
    }
}

