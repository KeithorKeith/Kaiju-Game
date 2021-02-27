using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    public GameObject helpBox;

    public void Close(bool keepCursor=true)
    {
        CloseHelp();
        gameObject.SetActive(false);
        if (!keepCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

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
