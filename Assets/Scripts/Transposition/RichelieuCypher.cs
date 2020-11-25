using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RichelieuCypher : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField outputField;

    public void Encript()
    {
        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;
        int numberOfLettersInBlock;
        int[] grid = GetRichelieuGrille(out numberOfLettersInBlock);
        int numberOfGrids = textLength % numberOfLettersInBlock == 0 ? textLength / numberOfLettersInBlock : textLength / numberOfLettersInBlock + 1;

        char[] charSet = { '?' };
        if (textToEncript != null && textToEncript.Length > 0)
        {
            if ((int)textToEncript[0] > 1000)
                charSet = Alphabets.russianWithSpace;
            else charSet = Alphabets.englishWithSpace;
        }

        System.Random random = new System.Random();
        int iterator = 0;
        StringBuilder sb = new StringBuilder();
        while (iterator < textLength)
        {
            for (int i = 0; i < numberOfGrids; i++)
            {
                for (int j = 0; j < grid.Length; j++)
                {
                    if (iterator < textToEncript.Length)
                    {
                        if (grid[j] == 1)
                        {
                            sb.Append(textToEncript[iterator]);
                            iterator++;
                        }
                        else
                            sb.Append(charSet[random.Next(0, charSet.Length)]);
                    }
                    else
                    {
                        if (grid[j] == 1)
                            sb.Append(' ');
                        else sb.Append(charSet[random.Next(0, charSet.Length)]);
                    }
                }
            }
        }

        outputField.text = sb.ToString();
    }

    public void Decript()
    {
        string textToEncript = inputField.text;
        int textLength = textToEncript.Length;
        int[] grid = GetRichelieuGrille(out _);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < textLength; i++)
        {
            int gridI = i % grid.Length;
            if (grid[gridI] == 1)
                sb.Append(textToEncript[i]);
        }

        outputField.text = sb.ToString().Trim();
    }

    private int[] GetRichelieuGrille(out int numberOfFreeHoles)
    {
        int[] grid = new int[70];
        int free = 0;
        System.Random random = new System.Random(70);
        bool holes = true;
        int iterator = 0;
        int randNumber = random.Next(1, 7);
        while (iterator < grid.Length)
        {
            if (holes)
            {
                grid[iterator] = 1;
                free++;
            }
            else grid[iterator] = 0;
            iterator++;
            randNumber--;
            if (randNumber <= 0) {
                if (holes)
                    randNumber = random.Next(5, 15);
                else randNumber = random.Next(1, 7);
                holes = !holes;
            }
        }
        numberOfFreeHoles = free;
        return grid;
    }
}
