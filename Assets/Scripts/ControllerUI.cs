using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControllerUI : MonoBehaviour
{
    [Header("Окна загрузки, смерти и выйгрыша")]
    [SerializeField]
    GameObject startWindow;
    [SerializeField]
    GameObject dieWindow;
    [SerializeField]
    GameObject endWindow;

    

    public void WindowsController(string value)
    {
        if (value == "start")
        {
            Time.timeScale = 1;
            startWindow.SetActive(false);
        }
        else if (value == "restart")
        {
            SceneManager.LoadScene("Finally_scene");
            Time.timeScale = 1;  
        }
        else if (value == "end")
        {
            Application.Quit();
        }
    }

    public void DieGame()
    {
        Time.timeScale = 0;
        dieWindow.SetActive(true);
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        endWindow.SetActive(true);
    }
}
