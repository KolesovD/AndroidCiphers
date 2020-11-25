using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitArrayValidator : MonoBehaviour
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
        if (char.IsDigit(ch))
            return ch;
        else if (ch == ',' && pos != 0 && char.IsDigit(text[pos - 1]))
            return ch;
        else if (ch == ' ' && pos != 0 && text[pos - 1] == ',')
            return ch;
        else return '\0';
    }
}
