using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurningCardanGrilleCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;
        int[] grid = GetCardanGrilleOrder(0);
        int numberOfLettersInGrid = grid.Length;
        int numberOfLettersInRow = (int) Mathf.Sqrt(grid.Length);
        int numberOfGrids = textLength % numberOfLettersInGrid == 0 ? textLength / numberOfLettersInGrid : textLength / numberOfLettersInGrid + 1;


        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberOfGrids; i++)
        {
            for (int j = 0; j < numberOfLettersInGrid; j++)
            {
                if (j != 0 && j % numberOfLettersInRow == 0)
                    sb.Append("\n");
                if ((i * numberOfLettersInGrid + grid[j]) < textLength)
                    sb.Append(textToEncript[i * numberOfLettersInGrid + grid[j]]);
                else sb.Append(' ');
            }
            if (i != numberOfGrids - 1)
                sb.Append("\n\n");
        }

        outputField.text = sb.ToString();
    }

    public void Decript()
    {
        string textToDecript = string.Join("", inputField.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));
        int textLength = textToDecript.Length;
        int[] grid = GetCardanGrilleOrder(0);
        int numberOfLettersInGrid = grid.Length;
        int numberOfGrids = textLength % numberOfLettersInGrid == 0 ? textLength / numberOfLettersInGrid : textLength / numberOfLettersInGrid + 1;
        int[] reverseGrid = new int[numberOfLettersInGrid];
        for (int i = 0; i < numberOfLettersInGrid; i++)
            reverseGrid[grid[i]] = i;

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberOfGrids; i++)
        {
            for (int j = 0; j < numberOfLettersInGrid; j++)
            {
                if ((i * numberOfLettersInGrid + reverseGrid[j]) < textLength)
                    sb.Append(textToDecript[i * numberOfLettersInGrid + reverseGrid[j]]);
                else sb.Append(' ');
            }
        }

        outputField.text = sb.ToString().Trim();
    }

    private int[] GetCardanGrilleOrder(int value)
    {
        switch (value)
        {
            default:
                return new int[16] { 1, 4, 10, 15, 2, 7, 9, 12, 0, 5, 11, 14, 3, 6, 8, 13 };
        }
    }
}
