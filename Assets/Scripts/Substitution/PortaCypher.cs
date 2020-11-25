using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class PortaCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField outputField;
    
    public void Encript()
    {
        string textToEncript = string.Join("", inputField.text.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        if (textToEncript.Length > 0)
        {
            Dictionary<string, List<char>> alphabets = new Dictionary<string, List<char>>();
            alphabets.Add("r", new List<char>(Alphabets.russian));
            alphabets.Add("e", new List<char>(Alphabets.english));

            CharEnumerator inCE = textToEncript.GetEnumerator();
            StringBuilder sb = new StringBuilder();

            char tempChar;
            KeyValuePair<string, char>? firstChar = null;
            bool moving = inCE.MoveNext();
            while (moving || firstChar.HasValue)
            {
                if (moving)
                {
                    tempChar = inCE.Current;
                    bool findList = false;
                    List<char> tempList;
                    foreach (string key in alphabets.Keys)
                    {
                        if (alphabets.TryGetValue(key, out tempList))
                        {
                            if (tempList.Contains(tempChar))
                            {
                                findList = true;
                                if (firstChar.HasValue)
                                {
                                    if (firstChar.Value.Key == key)
                                    {
                                        sb.Append(key);
                                        sb.AppendFormat("{0:00}", tempList.IndexOf(firstChar.Value.Value));
                                        sb.AppendFormat("{0:00}", tempList.IndexOf(tempChar));
                                        sb.Append(" ");
                                        firstChar = null;
                                        break;
                                    }
                                    else
                                    {
                                        outputField.text = "Неправильный входной текст";
                                        return;
                                    }
                                }
                                else
                                {
                                    firstChar = new KeyValuePair<string, char>(key, tempChar);
                                }
                            }
                        }
                    }
                    if (!findList)
                    {
                        outputField.text = "Неправильный входной текст";
                        return;
                    }
                }
                else
                {
                    if (firstChar.Value.Key == "r")
                        tempChar = 'х';
                    else tempChar = 'x';
                    if (alphabets.TryGetValue(firstChar.Value.Key, out List<char> tempList))
                    {
                        sb.Append(firstChar.Value.Key);
                        sb.AppendFormat("{0:00}", tempList.IndexOf(firstChar.Value.Value));
                        sb.AppendFormat("{0:00}", tempList.IndexOf(tempChar));
                    }
                    firstChar = null;
                }

                moving = inCE.MoveNext();
            }
            outputField.text = sb.ToString().Trim();
        }
    }

    public void Decript()
    {
        string[] textToEncript = inputField.text.Split(' ');
        Dictionary<string, List<char>> alphabets = new Dictionary<string, List<char>>();
        alphabets.Add("r", new List<char>(Alphabets.russian));
        alphabets.Add("e", new List<char>(Alphabets.english));
        StringBuilder sb = new StringBuilder();

        foreach (string pair in textToEncript)
        {
            List<char> tempList;
            if (alphabets.TryGetValue(pair[0].ToString(), out tempList))
            {
                if (int.TryParse((pair[1].ToString() + pair[2].ToString()), out int it1) && int.TryParse((pair[3].ToString() + pair[4].ToString()), out int it2))
                {
                    sb.Append(tempList[it1]);
                    sb.Append(tempList[it2]);
                }
                else
                {
                    outputField.text = "Неправильный входной текст";
                    return;
                }
            }
            else
            {
                outputField.text = "Неправильный входной текст";
                return;
            }
        }

        outputField.text = sb.ToString().Trim();
    }
}