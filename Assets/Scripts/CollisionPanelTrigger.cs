using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CollisionPanelTrigger : MonoBehaviour
{
    public static int collisionCount = 0; // Ka� kez �arp��ma oldu�unu takip eder
    public static HashSet<GameObject> collidedObjects = new HashSet<GameObject>(); // Hangi nesnelerle �arp���ld���n� takip eder

    public GameObject panel; // Her �arp��mada a��lacak panel
    public GameObject closingPanel; // 8. �arp��madan sonra a��lacak panel
    public Image targetImage; // �arp��mada de�i�ecek image
    public Sprite newSprite; // �arp��mada g�sterilecek resim
    public Image buttonTargetImage; // Butona bas�nca de�i�ecek image
    public Sprite buttonNewSprite; // Butona bas�nca g�sterilecek yeni resim
    public AudioSource audioSource; // Ses kayna��
    public AudioClip collisionSound; // �arp��ma sesi

    private bool isLastCollision = false; // 8. �arp��man�n olup olmad���n� takip eder
    private bool canCollide = true; // �arp��may� kontrol etmek i�in flag

    public GameObject targetGameObject; // Pasif hale getirilecek GameObject
    public GameObject activeOnCollisionkarartma;
    public GameObject activeOnCollision; // �arp��ma oldu�unda aktif olacak GameObject

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Panel ba�lang��ta kapal�
        }

        if (closingPanel != null)
        {
            closingPanel.SetActive(false); // Kapan�� paneli de ba�lang��ta kapal�
        }

        if (activeOnCollision != null)
        {
            activeOnCollision.SetActive(false); // Ba�lang��ta aktif olmayan GameObject
        }
        if (activeOnCollisionkarartma != null)
        {
            activeOnCollisionkarartma.SetActive(false); // Ba�lang��ta aktif olmayan GameObject
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!collidedObjects.Contains(other.gameObject) && canCollide) // E�er bu nesneyle daha �nce �arp��mad�ysa ve �arp��ma yap�lmas�na izin varsa
        {
            collidedObjects.Add(other.gameObject); // �arp���lan nesneyi listeye ekle
            collisionCount++; // �arp��ma say�s�n� art�r

            if (panel != null)
            {
                panel.SetActive(true); // Paneli a�
            }

            if (targetImage != null && newSprite != null)
            {
                targetImage.sprite = newSprite; // �arp��mada resmi de�i�tir
            }

            if (collisionCount == 8) // E�er 8. �arp��ma ise
            {
                isLastCollision = true;

                
            }

            // �arp��ma oldu�unda activeOnCollision GameObject'ini aktif hale getir
            if (activeOnCollisionkarartma != null)
            {
                activeOnCollisionkarartma.SetActive(true); // GameObject aktif hale gelir
            }
            if (activeOnCollision != null)
            {
                activeOnCollision.SetActive(true); // GameObject aktif hale gelir
            }
            Time.timeScale = 0f; // Oyun durur

            // 1 saniyeli�ine �arp��ma engelle
            StartCoroutine(DisableCollisionTemporarily());
        }
    }

    private IEnumerator DisableCollisionTemporarily()
    {
        canCollide = false; // �arp��ma kontrol�n� devre d��� b�rak
        yield return new WaitForSeconds(6f); // 1 saniye bekle
        canCollide = true; // �arp��ma kontrol�n� tekrar aktif et
    }

    public void CloseOrOpenFinalPanel() // Panel kapatma butonuna bas�l�nca �al��acak
    {
        if (panel != null)
        {
            panel.SetActive(false); // Paneli kapat
        }

        if (isLastCollision && closingPanel != null) // E�er 8. �arp��ma ger�ekle�mi�se
        {
            closingPanel.SetActive(true);
            // 8. �arp��ma ger�ekle�ti�inde hedef GameObject'i pasif hale getir
            if (targetGameObject != null)
            {
                targetGameObject.SetActive(false); // GameObject'i pasif hale getir
            }// Kapan�� panelini a�
        }

        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound); // Ses burada �alacak
        }

        if (buttonTargetImage != null && buttonNewSprite != null)
        {
            buttonTargetImage.sprite = buttonNewSprite; // Butona bas�nca resmi de�i�tir
        }

        // Butona bas�ld���nda activeOnCollision GameObject'ini tekrar pasif hale getir
        if (activeOnCollision != null)
        {
            activeOnCollision.SetActive(false); // GameObject'i pasif hale getir
        }
        if (activeOnCollisionkarartma != null)
        {
            activeOnCollisionkarartma.SetActive(false); // GameObject'i pasif hale getir
        }

        Time.timeScale = 1f; // Oyunu devam ettir
        collidedObjects.Clear();
    }
}
