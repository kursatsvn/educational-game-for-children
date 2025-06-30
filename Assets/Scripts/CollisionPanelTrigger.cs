using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CollisionPanelTrigger : MonoBehaviour
{
    public static int collisionCount = 0; // Kaç kez çarpýþma olduðunu takip eder
    public static HashSet<GameObject> collidedObjects = new HashSet<GameObject>(); // Hangi nesnelerle çarpýþýldýðýný takip eder

    public GameObject panel; // Her çarpýþmada açýlacak panel
    public GameObject closingPanel; // 8. çarpýþmadan sonra açýlacak panel
    public Image targetImage; // Çarpýþmada deðiþecek image
    public Sprite newSprite; // Çarpýþmada gösterilecek resim
    public Image buttonTargetImage; // Butona basýnca deðiþecek image
    public Sprite buttonNewSprite; // Butona basýnca gösterilecek yeni resim
    public AudioSource audioSource; // Ses kaynaðý
    public AudioClip collisionSound; // Çarpýþma sesi

    private bool isLastCollision = false; // 8. çarpýþmanýn olup olmadýðýný takip eder
    private bool canCollide = true; // Çarpýþmayý kontrol etmek için flag

    public GameObject targetGameObject; // Pasif hale getirilecek GameObject
    public GameObject activeOnCollisionkarartma;
    public GameObject activeOnCollision; // Çarpýþma olduðunda aktif olacak GameObject

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Panel baþlangýçta kapalý
        }

        if (closingPanel != null)
        {
            closingPanel.SetActive(false); // Kapanýþ paneli de baþlangýçta kapalý
        }

        if (activeOnCollision != null)
        {
            activeOnCollision.SetActive(false); // Baþlangýçta aktif olmayan GameObject
        }
        if (activeOnCollisionkarartma != null)
        {
            activeOnCollisionkarartma.SetActive(false); // Baþlangýçta aktif olmayan GameObject
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!collidedObjects.Contains(other.gameObject) && canCollide) // Eðer bu nesneyle daha önce çarpýþmadýysa ve çarpýþma yapýlmasýna izin varsa
        {
            collidedObjects.Add(other.gameObject); // Çarpýþýlan nesneyi listeye ekle
            collisionCount++; // Çarpýþma sayýsýný artýr

            if (panel != null)
            {
                panel.SetActive(true); // Paneli aç
            }

            if (targetImage != null && newSprite != null)
            {
                targetImage.sprite = newSprite; // Çarpýþmada resmi deðiþtir
            }

            if (collisionCount == 8) // Eðer 8. çarpýþma ise
            {
                isLastCollision = true;

                
            }

            // Çarpýþma olduðunda activeOnCollision GameObject'ini aktif hale getir
            if (activeOnCollisionkarartma != null)
            {
                activeOnCollisionkarartma.SetActive(true); // GameObject aktif hale gelir
            }
            if (activeOnCollision != null)
            {
                activeOnCollision.SetActive(true); // GameObject aktif hale gelir
            }
            Time.timeScale = 0f; // Oyun durur

            // 1 saniyeliðine çarpýþma engelle
            StartCoroutine(DisableCollisionTemporarily());
        }
    }

    private IEnumerator DisableCollisionTemporarily()
    {
        canCollide = false; // Çarpýþma kontrolünü devre dýþý býrak
        yield return new WaitForSeconds(6f); // 1 saniye bekle
        canCollide = true; // Çarpýþma kontrolünü tekrar aktif et
    }

    public void CloseOrOpenFinalPanel() // Panel kapatma butonuna basýlýnca çalýþacak
    {
        if (panel != null)
        {
            panel.SetActive(false); // Paneli kapat
        }

        if (isLastCollision && closingPanel != null) // Eðer 8. çarpýþma gerçekleþmiþse
        {
            closingPanel.SetActive(true);
            // 8. çarpýþma gerçekleþtiðinde hedef GameObject'i pasif hale getir
            if (targetGameObject != null)
            {
                targetGameObject.SetActive(false); // GameObject'i pasif hale getir
            }// Kapanýþ panelini aç
        }

        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound); // Ses burada çalacak
        }

        if (buttonTargetImage != null && buttonNewSprite != null)
        {
            buttonTargetImage.sprite = buttonNewSprite; // Butona basýnca resmi deðiþtir
        }

        // Butona basýldýðýnda activeOnCollision GameObject'ini tekrar pasif hale getir
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
