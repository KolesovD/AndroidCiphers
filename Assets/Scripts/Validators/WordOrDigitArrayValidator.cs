using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordOrDigitArrayValidator : MonoBehaviour
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
        
        if ((char.IsDigit(ch) && pos == 0) || (char.IsDigit(ch) && char.IsDigit(text[0])))
            return ch;
        else if (ch == ',' && pos != 0 && char.IsDigit(text[pos - 1]))
            return ch;
        else if (ch == ' ' && pos != 0 && text[pos - 1] == ',')
            return ch;
        else if ((char.IsLetter(ch) && pos == 0) || (char.IsLetter(ch) && char.IsLetter(text[0])))
            return ch;
        else return '\0';
    }
}
