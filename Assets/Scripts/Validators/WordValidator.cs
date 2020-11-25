using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


class WordValidator : MonoBehaviour
{
    void Awake()
    {
        TMP_InputField input = GetComponent<TMP_InputField>();
        if (input)
        {
            input.onValidateInput = Validate;
        }
    }

    public char Validate(string text, int pos, char ch)
    {

        if (char.IsLetter(ch) || ch == ' ')
            return ch;
        else return '\0';
    }
}
