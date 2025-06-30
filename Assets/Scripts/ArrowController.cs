using UnityEngine;
using System.Collections; // Bu sat�r� ekleyin


public class ArrowController : MonoBehaviour
{
    public float moveAmount = 100f; // Okun hareket miktar� (y ekseninde)
    public GameObject panel; // A��lacak panel
    private bool isPanelOpen = false; // Panelin a��k olup olmad���n� kontrol etmek i�in


    // �arp��ma s�ras�nda ok hareketi
    public void MoveArrow(bool isCorrect)
    {
        if (isCorrect)
        {
            // Do�ru besin �arp��t�ysa ok yukar� hareket eder
            transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
        }
        else
        {
            // Yanl�� besin �arp��t�ysa ok a�a�� hareket eder
            transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
        }

        // Debug log ile ok pozisyonunu kontrol edebilirsiniz
        Debug.Log("Ok pozisyonu: " + transform.position.y);
    }
    void Update()
    {
        // Okun pozisyonunu yukar� hareket ettirme kodu
        if (transform.position.y >= 915 && !isPanelOpen) // Ok 375'e geldi�inde paneli a�
        {
            OpenPanel();
        }
    }
    void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Paneli a�
            isPanelOpen = true; // Panelin a��ld���n� i�aretle
            Time.timeScale = 0f; // Zaman� durdur
            DestroyObjectsWithTag("UnhealthyFood");
            DestroyObjectsWithTag("HealthyFood");
            // Coroutine ba�lat�yoruz, 5 saniye sonra devam et

        }
    }
    void DestroyObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
    // Oyunu 5 saniye sonra devam ettir
    IEnumerator ResumeGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Ger�ek zamanla bekle (timeScale etkilenmez)
        Time.timeScale = 1f; // Zaman� ba�lat
    }

    // Paneli kapat ve oyunu ba�lat
    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Paneli kapat
            isPanelOpen = false; // Panelin kapal� oldu�unu i�aretle
            Time.timeScale = 1f; // Zaman� ba�lat
        }
    }
}
