using UnityEngine;
using UnityEngine.UI;

public class PauseOnSceneLoad : MonoBehaviour
{
    public GameObject pausePanel;

    void Start()
    {
        Time.timeScale = 0f; // Oyun duruyor
        if (pausePanel != null)
        {
            pausePanel.SetActive(true); // UI paneli görünür yap
        }
    }
}
