using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ClickToActivatePanel : MonoBehaviour, IPointerClickHandler
{
    public static int clickedCount = 0; // Kaç UI Image'a týklandýðýný takip eder
    public static HashSet<GameObject> clickedImages = new HashSet<GameObject>(); // Hangi Image’lerin týklandýðýný takip eder

    public GameObject panel; // Her týklamada açýlacak panel
    public GameObject closingPanel; // 8. týklamadan sonra kapatma butonuna basýlýnca açýlacak panel
    public Image targetImage; // Deðiþecek image
    public Sprite newSprite; // Yeni resim
    public Button closeButton; // Panel içindeki kapatma butonu
    public AudioSource audioSource; // Ses kaynaðý
    public AudioClip clickSound; // Týklama sesi

    private bool isLastImage = false; // 8. UI Image týklanýp týklanmadýðýný takip eder

    void Start()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseOrOpenFinalPanel); // Kapatma butonuna event ekle
    }

    public void OnPointerClick(PointerEventData eventData) // UI Image'a týklanýrsa çalýþýr
    {
        if (!clickedImages.Contains(gameObject)) // Eðer daha önce týklanmadýysa
        {
            clickedImages.Add(gameObject); // Bu nesneyi ekle
            clickedCount++; // Sayaç artýr

            if (audioSource != null && clickSound != null)
                audioSource.PlayOneShot(clickSound); // Ses çal

            if (panel != null)
                panel.SetActive(true); // Paneli aç

            if (targetImage != null && newSprite != null)
                targetImage.sprite = newSprite; // Resmi deðiþtir

            if (clickedCount == 8) // Eðer 8. UI Image'e týklanmýþsa
            {
                isLastImage = true;
            }
        }
    }

    void CloseOrOpenFinalPanel() // Panel kapatma butonuna basýlýnca çalýþacak
    {
        if (panel != null)
            panel.SetActive(false); // Paneli kapat

        if (isLastImage && closingPanel != null) // Eðer 8. UI Image'e týklanmýþsa
        {
            closingPanel.SetActive(true); // Kapanýþ panelini aç
        }
        Image imgComponent = GetComponent<Image>(); // UI Image içindeki Image bileþenini al
        if (imgComponent != null)
        {
            imgComponent.enabled = false; // Sadece Image bileþenini kapat (GameObject pasif kalýr)
        }
    }
}
