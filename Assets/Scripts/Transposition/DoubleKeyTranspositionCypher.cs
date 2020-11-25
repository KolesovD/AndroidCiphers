using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoubleKeyTranspositionCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField horizontalKeyField;
    [SerializeField] TMP_InputField verticalKeyField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        if (!StaticValidator.ValidateWDKey(horizontalKeyField, outputField, out int horizontalKeyArrayLength, out int[] horizontalKeyIntArray))
            return;

        if (!StaticValidator.ValidateWDKey(verticalKeyField, outputField, out int verticalKeyArrayLength, out int[] verticalKeyIntArray))
            return;

        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;
        int numberOfRows = textLength % horizontalKeyArrayLength == 0 ? textLength / horizontalKeyArrayLength : textLength / horizontalKeyArrayLength + 1;
        int squareMultiplier = numberOfRows % verticalKeyArrayLength == 0 ? numberOfRows / verticalKeyArrayLength : numberOfRows / verticalKeyArrayLength + 1;
        StringBuilder sb = new StringBuilder();
        for (int m = 0; m < squareMultiplier; m++)
        {
            int quareShift = m * horizontalKeyArrayLength * verticalKeyArrayLength;
            for (int i = 0; i < /*numberOfRows*/verticalKeyArrayLength; i++)
            {
                for (int j = 0; j < horizontalKeyArrayLength; j++)
                {
                    if ((quareShift + verticalKeyIntArray[i] * horizontalKeyArrayLength + horizontalKeyIntArray[j]) >= textLength)
                        sb.Append(' ');
                    else sb.Append(textToEncript[quareShift + verticalKeyIntArray[i] * horizontalKeyArrayLength + horizontalKeyIntArray[j]]);
                }
            }
        }

        outputField.text = sb.ToString().Trim();
    }

    public void Decript()
    {
        if (!StaticValidator.ValidateWDKey(horizontalKeyField, outputField, out int horizontalKeyArrayLength, out int[] horizontalKeyIntArray))
            return;

        if (!StaticValidator.ValidateWDKey(verticalKeyField, outputField, out int verticalKeyArrayLength, out int[] verticalKeyIntArray))
            return;

        int[] horizontalReverseKeyIntArray = new int[horizontalKeyIntArray.Length];
        for (int i = 0; i < horizontalKeyIntArray.Length; i++)
            horizontalReverseKeyIntArray[horizontalKeyIntArray[i]] = i;

        int[] verticalReverseKeyIntArray = new int[verticalKeyIntArray.Length];
        for (int i = 0; i < verticalKeyIntArray.Length; i++)
            verticalReverseKeyIntArray[verticalKeyIntArray[i]] = i;

        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;
        int numberOfRows = textLength % horizontalKeyArrayLength == 0 ? textLength / horizontalKeyArrayLength : textLength / horizontalKeyArrayLength + 1;
        int squareMultiplier = numberOfRows % verticalKeyArrayLength == 0 ? numberOfRows / verticalKeyArrayLength : numberOfRows / verticalKeyArrayLength + 1;
        StringBuilder sb = new StringBuilder();
        for (int m = 0; m < squareMultiplier; m++)
        {
            int quareShift = m * horizontalKeyArrayLength * verticalKeyArrayLength;
            for (int i = 0; i < /*numberOfRows*/verticalKeyArrayLength; i++)
            {
                for (int j = 0; j < horizontalKeyArrayLength; j++)
                {
                    if ((quareShift + verticalReverseKeyIntArray[i] * horizontalKeyArrayLength + horizontalReverseKeyIntArray[j]) >= textLength)
                        sb.Append(' ');
                    else sb.Append(textToEncript[quareShift + verticalReverseKeyIntArray[i] * horizontalKeyArrayLength + horizontalReverseKeyIntArray[j]]);
                }
            }
        }

        outputField.text = sb.ToString().Trim();
    }

    
}
