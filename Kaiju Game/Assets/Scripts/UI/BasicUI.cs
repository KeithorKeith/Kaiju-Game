using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    public GameObject helpBox;

    public void Close()
    {
        CloseHelp();
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenHelp()
    {
        helpBox.SetActive(true);
    }

    public void CloseHelp()
    {
        if(helpBox != null)
        {
            helpBox.SetActive(false);
        }
    }

    public void OpenMenu(GameObject menuObject)
    {
        menuObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
