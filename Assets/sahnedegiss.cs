using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sahnedegiss : MonoBehaviour
{
    public void Sahnedegis()
    {
        SceneManager.LoadScene(1);
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
}
