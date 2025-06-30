using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    public GameObject pausePanel;

    public void Resume()
    {
        Time.timeScale = 1f; // Oyun devam eder
        Debug.Log("Time.timeScale: " + Time.timeScale); // Konsolda Time.timeScale de�erini kontrol et

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Pause paneli kapat
        }
    }
    void Update()
    {
        Debug.Log("G�ncel Time.timeScale: " + Time.timeScale);
    }
}
