using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class PlayfairCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField keyField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        DoPlayfair(true);
    }

    public void Decript()
    {
        DoPlayfair(false);
    }

    private void DoPlayfair(bool adding)
    {
        string key = keyField.text;
        string textToEncript = string.Join("", inputField.text.Replace('j', 'i').Replace('J', 'I').Replace('ё', 'е').Replace('Ё', 'Е').Split(new char[] { ' ' }));
        if (key != null && key.Length != 0)
        {
            key = key.Replace('j', 'i').Replace('J', 'I').Replace('ё', 'е').Replace('Ё', 'Е');
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
                int symbolsInRow;
                int rowsInmatrix;
                if (transKey.Count == 25)
                {
                    symbolsInRow = 5;
                    rowsInmatrix = 5;
                }
                else
                {
                    symbolsInRow = 8;
                    rowsInmatrix = 4;
                }
                char? firstChar = null;
                bool moved = inCE.MoveNext();
                while (moved || firstChar.HasValue)
                {
                    if (moved && !transKey.Contains(char.ToLower(inCE.Current)))
                    {
                        outputField.text = "Неправильный входной текст";
                        return;
                    }
                    if (!firstChar.HasValue)
                        firstChar = moved ? inCE.Current : transKey.Count == 25 ? 'x' : 'х';
                    else
                    {
                        tempChar = moved ? inCE.Current : transKey.Count == 25 ? 'x' : 'х';
                        char[] bigram = new char[2];
                        if (char.ToLower(tempChar) == char.ToLower(firstChar.Value))
                        {
                            bigram[0] = tempChar;
                            bigram[1] = transKey.Count == 25 ? 'x' : 'х';
                        }
                        else
                        {
                            bigram[0] = firstChar.Value;
                            bigram[1] = tempChar;
                            firstChar = null;
                        }
                        int X0 = transKey.IndexOf(char.ToLower(bigram[0]));
                        int Y0 = X0 / symbolsInRow;
                        X0 = X0 % symbolsInRow;
                        int X1 = transKey.IndexOf(char.ToLower(bigram[1]));
                        int Y1 = X1 / symbolsInRow;
                        X1 = X1 % symbolsInRow;
                        if (Y0 == Y1)
                        {
                            X0 = adding ? ++X0 % symbolsInRow : (--X0 + symbolsInRow) % symbolsInRow;
                            X1 = adding ? ++X1 % symbolsInRow : (--X1 + symbolsInRow) % symbolsInRow;
                        }
                        else if (X0 == X1)
                        {
                            Y0 = adding ? ++Y0 % rowsInmatrix : (--Y0 + symbolsInRow) % rowsInmatrix;
                            Y1 = adding ? ++Y1 % rowsInmatrix : (--Y1 + symbolsInRow) % rowsInmatrix;
                        }
                        else
                        {
                            int tempX = X0;
                            X0 = X1;
                            X1 = tempX;
                        }
                        sb.Append(char.IsLower(bigram[0]) ? transKey[Y0 * symbolsInRow + X0] : char.ToUpper(transKey[Y0 * symbolsInRow + X0]));
                        sb.Append(char.IsLower(bigram[1]) ? transKey[Y1 * symbolsInRow + X1] : char.ToUpper(transKey[Y1 * symbolsInRow + X1]));
                    }
                    moved = inCE.MoveNext();
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
        List<char> russian = new List<char>(Alphabets.russianPlayfair);
        List<char> english = new List<char>(Alphabets.englishPlayfair);
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

