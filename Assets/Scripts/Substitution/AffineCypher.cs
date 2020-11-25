using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


class AffineCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField keyAField;
    [SerializeField] TMP_InputField keyBField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        int keyA = keyAField.text.Length == 0 ? 0 : int.Parse(keyAField.text);
        int keyB = keyBField.text.Length == 0 ? 0 : int.Parse(keyBField.text);
        string textToEncript = inputField.text;

        List<char> currentAlphabet;
        if (ValidateKey(keyA, ref textToEncript, out currentAlphabet))
        {
            CharEnumerator inCE = textToEncript.GetEnumerator();
            StringBuilder sb = new StringBuilder();

            char tempChar;
            int alphabetLenth = currentAlphabet.Count;
            while (inCE.MoveNext())
            {
                tempChar = inCE.Current;
                if (currentAlphabet.Contains(char.ToLower(tempChar)))
                {
                    sb.Append(char.IsLower(tempChar) ? currentAlphabet[(keyA * currentAlphabet.IndexOf(char.ToLower(tempChar)) + keyB) % alphabetLenth] : char.ToUpper(currentAlphabet[(keyA * currentAlphabet.IndexOf(char.ToLower(tempChar)) + keyB) % alphabetLenth]));
                }
                else sb.Append(tempChar);
            }
            outputField.text = sb.ToString();
        }
        else
        {
            if (currentAlphabet == null)
                outputField.text = "Ошибка считывания текста.";
            else outputField.text = "Неправильный ключ. Переменная 'a' не должна нацело делиться на " + currentAlphabet.Count;
        }
    }

    public void Decript()
    {
        int keyA = keyAField.text.Length == 0 ? 0 : int.Parse(keyAField.text);
        int keyB = keyBField.text.Length == 0 ? 0 : int.Parse(keyBField.text);
        string textToEncript = inputField.text;

        List<char> currentAlphabet;
        if (ValidateKey(keyA, ref textToEncript, out currentAlphabet))
        {
            CharEnumerator inCE = textToEncript.GetEnumerator();
            StringBuilder sb = new StringBuilder();

            char tempChar;
            int alphabetLenth = currentAlphabet.Count;
            int aMinus = CalculateAMinus(keyA, alphabetLenth);
            while (inCE.MoveNext())
            {
                tempChar = inCE.Current;
                if (currentAlphabet.Contains(char.ToLower(tempChar)))
                {
                    sb.Append(char.IsLower(tempChar) ? currentAlphabet[(aMinus * (currentAlphabet.IndexOf(char.ToLower(tempChar)) + alphabetLenth - keyB)) % alphabetLenth] : char.ToUpper(currentAlphabet[(aMinus * (currentAlphabet.IndexOf(char.ToLower(tempChar)) + alphabetLenth - keyB)) % alphabetLenth]));
                }
                else sb.Append(tempChar);
            }
            outputField.text = sb.ToString();
        }
        else
        {
            if (currentAlphabet == null)
                outputField.text = "Ошибка считывания текста.";
            else outputField.text = "Неправильный ключ. Переменная 'a' не должна нацело делиться на " + currentAlphabet.Count;
        };
    }

    private bool ValidateKey(int keyA, ref string text, out List<char> alphabet)
    {
        List<char> russian = new List<char>(Alphabets.russian);
        List<char> english = new List<char>(Alphabets.english);
        List<char> currentAlphabet;
        if (russian.Contains(char.ToLower(text[0])))
        {
            currentAlphabet = russian;
            alphabet = russian;
        }
        else if (english.Contains(char.ToLower(text[0])))
        {
            currentAlphabet = english;
            alphabet = english;
        }
        else
        {
            alphabet = null;
            return false;
        }

        if (keyA != 1 && (keyA < 1 || currentAlphabet.Count % keyA == 0))
            return false;
        return true;
    }

    private int CalculateAMinus(int a, int m)
    {
        int aMinus = 1;
        while (((a * aMinus) % m) != 1)
            aMinus++;
        return aMinus;
    }
}
