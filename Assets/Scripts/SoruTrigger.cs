using UnityEngine;

public class SoruTrigger : MonoBehaviour
{
    public GameObject soruPaneli; // UI Panelini buraya sürükle

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player nesnesinin tag'ý "Player" olmalý
        {
            soruPaneli.SetActive(true); // Paneli aç
            Time.timeScale = 0f; // Oyunu durdur (isteðe baðlý)
            gameObject.SetActive(false); // Objeyi pasif hale getir

        }
    }

    public void SoruKapat()
    {
        soruPaneli.SetActive(false); // Paneli kapat
        Time.timeScale = 1f; // Oyunu devam ettir
    }
}
