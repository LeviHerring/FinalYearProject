using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuButtons : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene(1); 

    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void OpenLinkQuiz()
    {
        Process.Start(new ProcessStartInfo("https://docs.google.com/forms/d/e/1FAIpQLScw1mV6uDJHqBxyDJ3jQQ6Ys2aPkycZ8vTr3iGvwbbhGFasZA/viewform?usp=dialog") { UseShellExecute = true });
    }

    public void OpenLinkSurvey()
    {
        Process.Start(new ProcessStartInfo("https://docs.google.com/forms/d/e/1FAIpQLScw1mV6uDJHqBxyDJ3jQQ6Ys2aPkycZ8vTr3iGvwbbhGFasZA/viewform?usp=dialog") { UseShellExecute = true });


    }
}
