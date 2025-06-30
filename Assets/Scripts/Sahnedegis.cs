using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sahnedegis : MonoBehaviour
{
    public void maden()
    {
        SceneManager.LoadScene(1);
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
    public void anamenu()
    {
        SceneManager.LoadScene(0);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Time.timeScale = 1f;


    }
    public void fosil()
    {
        SceneManager.LoadScene(2);
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
    public void dunya()
    {
        SceneManager.LoadScene(3);
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
    public void besin()
    {
        SceneManager.LoadScene(4);
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
    public void saglik()
    {
        SceneManager.LoadScene(6);
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
}
