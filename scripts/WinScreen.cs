using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void Setup()
    {
        //Debug.Log("setup");
        gameObject.SetActive(true);
    }

    public void ContinueButton()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void WinMainMenuButton()
    {
        SceneManager.LoadScene(1);
    }

    public void WinQuitToDesktopButton()
    {
        Application.Quit();
    }
}
