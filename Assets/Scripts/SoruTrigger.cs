using UnityEngine;

public class SoruTrigger : MonoBehaviour
{
    public GameObject soruPaneli; // UI Panelini buraya s�r�kle

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player nesnesinin tag'� "Player" olmal�
        {
            soruPaneli.SetActive(true); // Paneli a�
            Time.timeScale = 0f; // Oyunu durdur (iste�e ba�l�)
            gameObject.SetActive(false); // Objeyi pasif hale getir

        }
    }

    public void SoruKapat()
    {
        soruPaneli.SetActive(false); // Paneli kapat
        Time.timeScale = 1f; // Oyunu devam ettir
    }
}
