using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject[] windows;

    public void ActivateWindow(int number)
    {
        if (windows != null && number < windows.Length)
        {
            for (int i = 0; i < windows.Length; i++)
            {
                if (i != number)
                {
                    if (windows[i].activeSelf)
                        windows[i].SetActive(false);
                }
                else if (!windows[i].activeSelf)
                    windows[i].SetActive(true);
            }
        }
    }
}
