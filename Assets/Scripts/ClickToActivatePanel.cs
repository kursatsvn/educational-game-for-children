using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ClickToActivatePanel : MonoBehaviour, IPointerClickHandler
{
    public static int clickedCount = 0; // Ka� UI Image'a t�kland���n� takip eder
    public static HashSet<GameObject> clickedImages = new HashSet<GameObject>(); // Hangi Image�lerin t�kland���n� takip eder

    public GameObject panel; // Her t�klamada a��lacak panel
    public GameObject closingPanel; // 8. t�klamadan sonra kapatma butonuna bas�l�nca a��lacak panel
    public Image targetImage; // De�i�ecek image
    public Sprite newSprite; // Yeni resim
    public Button closeButton; // Panel i�indeki kapatma butonu
    public AudioSource audioSource; // Ses kayna��
    public AudioClip clickSound; // T�klama sesi

    private bool isLastImage = false; // 8. UI Image t�klan�p t�klanmad���n� takip eder

    void Start()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseOrOpenFinalPanel); // Kapatma butonuna event ekle
    }

    public void OnPointerClick(PointerEventData eventData) // UI Image'a t�klan�rsa �al���r
    {
        if (!clickedImages.Contains(gameObject)) // E�er daha �nce t�klanmad�ysa
        {
            clickedImages.Add(gameObject); // Bu nesneyi ekle
            clickedCount++; // Saya� art�r

            if (audioSource != null && clickSound != null)
                audioSource.PlayOneShot(clickSound); // Ses �al

            if (panel != null)
                panel.SetActive(true); // Paneli a�

            if (targetImage != null && newSprite != null)
                targetImage.sprite = newSprite; // Resmi de�i�tir

            if (clickedCount == 8) // E�er 8. UI Image'e t�klanm��sa
            {
                isLastImage = true;
            }
        }
    }

    void CloseOrOpenFinalPanel() // Panel kapatma butonuna bas�l�nca �al��acak
    {
        if (panel != null)
            panel.SetActive(false); // Paneli kapat

        if (isLastImage && closingPanel != null) // E�er 8. UI Image'e t�klanm��sa
        {
            closingPanel.SetActive(true); // Kapan�� panelini a�
        }
        Image imgComponent = GetComponent<Image>(); // UI Image i�indeki Image bile�enini al
        if (imgComponent != null)
        {
            imgComponent.enabled = false; // Sadece Image bile�enini kapat (GameObject pasif kal�r)
        }
    }
}
