using UnityEngine;
using System.Collections; // Bu satýrý ekleyin


public class ArrowController : MonoBehaviour
{
    public float moveAmount = 100f; // Okun hareket miktarý (y ekseninde)
    public GameObject panel; // Açýlacak panel
    private bool isPanelOpen = false; // Panelin açýk olup olmadýðýný kontrol etmek için


    // Çarpýþma sýrasýnda ok hareketi
    public void MoveArrow(bool isCorrect)
    {
        if (isCorrect)
        {
            // Doðru besin çarpýþtýysa ok yukarý hareket eder
            transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
        }
        else
        {
            // Yanlýþ besin çarpýþtýysa ok aþaðý hareket eder
            transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
        }

        // Debug log ile ok pozisyonunu kontrol edebilirsiniz
        Debug.Log("Ok pozisyonu: " + transform.position.y);
    }
    void Update()
    {
        // Okun pozisyonunu yukarý hareket ettirme kodu
        if (transform.position.y >= 915 && !isPanelOpen) // Ok 375'e geldiðinde paneli aç
        {
            OpenPanel();
        }
    }
    void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Paneli aç
            isPanelOpen = true; // Panelin açýldýðýný iþaretle
            Time.timeScale = 0f; // Zamaný durdur
            DestroyObjectsWithTag("UnhealthyFood");
            DestroyObjectsWithTag("HealthyFood");
            // Coroutine baþlatýyoruz, 5 saniye sonra devam et

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
        yield return new WaitForSecondsRealtime(delay); // Gerçek zamanla bekle (timeScale etkilenmez)
        Time.timeScale = 1f; // Zamaný baþlat
    }

    // Paneli kapat ve oyunu baþlat
    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Paneli kapat
            isPanelOpen = false; // Panelin kapalý olduðunu iþaretle
            Time.timeScale = 1f; // Zamaný baþlat
        }
    }
}
