using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VigenereTranslitCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField keyField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        if (keyField.text.Length != 0)
        {
            List<int> key = ValidateKey(keyField.text.ToLower());
            if (key != null)
                DoCesar(key, true);
            else outputField.text = "Неправильный ключ";
        }
        else outputField.text = inputField.text;
    }

    public void Decript()
    {
        if (keyField.text.Length != 0)
        {
            List<int> key = ValidateKey(keyField.text.ToLower());
            if (key != null)
                DoCesar(key, false);
            else outputField.text = "Неправильный ключ";
        }
        else outputField.text = inputField.text;
    }

    private void DoCesar(List<int> key, bool encript)
    {
        string textToEncript = inputField.text.ToLower();
        if (textToEncript.Length > 0)
        {
            StringBuilder sbTranslit = new StringBuilder();
            StringBuilder newString = new StringBuilder();

            List<char> english = new List<char>(Alphabets.english);

            List<string> translitKeys = new List<string>(new string[] { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" });
            List<string> translitValues = new List<string>(new string[] { "a", "b", "v", "g", "d", "ye", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h", "c", "ch", "sh", "w", "j", "ji", "q", "e", "yu", "ya" });

            int keyLength = key.Count;
            for (int i = 0; i < keyLength; i++)
                Debug.Log("key[" + i + "] = " + key[i]);
            int currentKey = 0;

            char tempChar;
            int textLength = textToEncript.Length;

            for (int i = 0; i < textLength; i++)
            {
                string nextChar = textToEncript[i].ToString();
                if (translitKeys.Contains(nextChar))
                    newString.Append(translitValues[translitKeys.IndexOf(nextChar)]);
                else newString.Append(nextChar);
            }

            string translitString = newString.ToString();
            Debug.Log(translitString);
            CharEnumerator inCE = translitString.GetEnumerator();
            newString.Clear();

            while (inCE.MoveNext())
            {
                tempChar = inCE.Current;
                int tempShift;
                if (english.Contains(tempChar))
                {
                    //Debug.Log("english Contains " + tempChar);
                    tempShift = encript ? key[currentKey] : -key[currentKey];
                    //Debug.Log("currentKey: " + currentKey);
                    while (tempShift < 0)
                        tempShift += english.Count;
                    tempShift = tempShift % english.Count;
                    sbTranslit.Append(english[(english.IndexOf(tempChar) + tempShift) % english.Count]);
                    if (++currentKey >= keyLength)
                        currentKey = 0;
                    //Debug.Log("currentKey: " + currentKey);
                }
                else sbTranslit.Append(tempChar);
            }

            string sbTranslitString = sbTranslit.ToString();
            Debug.Log(sbTranslitString);

            for (int i = 0; i < sbTranslitString.Length; i++)
            {
                if (i < sbTranslitString.Length - 1)
                {
                    string twoLetters = string.Concat(sbTranslitString[i], sbTranslitString[i + 1]);
                    if (translitValues.Contains(twoLetters))
                    {
                        newString.Append(translitKeys[translitValues.IndexOf(twoLetters)]);
                        i++;
                    }
                    else
                    {
                        string nextChar = sbTranslitString[i].ToString();
                        if (translitValues.Contains(nextChar))
                        {
                            newString.Append(translitKeys[translitValues.IndexOf(nextChar)]);
                        }
                        else newString.Append(nextChar);
                    }
                }
                else
                {
                    string nextChar = sbTranslitString[i].ToString();
                    if (translitValues.Contains(nextChar))
                    {
                        newString.Append(translitKeys[translitValues.IndexOf(nextChar)]);
                    }
                    else newString.Append(nextChar);
                }
            }
            Debug.Log(newString.ToString());

            outputField.text = newString.ToString();
        }
        else outputField.text = "";
    }

    private List<int> ValidateKey(string key)
    {
        key = string.Join("", key.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        List<int> intKey = new List<int>();
        List<char> alphabet = new List<char>(Alphabets.russian);
        for (int i = 0; i < key.Length; i++)
        {
            bool find = false;
            if (alphabet.Contains(key[i]))
            {
                intKey.Add(alphabet.IndexOf(key[i]));
                find = true;
            }
            if (!find)
                return null;
        }
        return intKey;
    }
}