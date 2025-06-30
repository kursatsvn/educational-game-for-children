using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelSpin : MonoBehaviour
{
    public RectTransform wheel; // Çarkýn RectTransform'u
    public Button spinButton;   // Dönme butonu
    public GameObject[] panels; // Açýlacak 15 farklý panel
    public GameObject endPanel; // Oyun bitiþ paneli

    private List<int> openedPanels = new List<int>(); // Açýlmýþ panellerin indeksleri
    private bool isSpinning = false;
    private int lastPanelIndex = -1; // 15. panelin indeksini saklayacaðýz

    void Start()
    {
        spinButton.onClick.AddListener(SpinWheel);
        HideAllPanels();
        Debug.Log("Oyun Baþladý! Tüm paneller gizlendi.");

        // Event'i temizle (eðer önceden kalmýþsa)
        panelkapat.OnPanelClosed -= OnLastPanelClosedViaEvent;
    }

    void OnDestroy()
    {
        panelkapat.OnPanelClosed -= OnLastPanelClosedViaEvent;
    }

    void SpinWheel()
    {
        if (!isSpinning && openedPanels.Count < panels.Length)
        {
            Debug.Log("Çark Döndürülüyor...");
            isSpinning = true;
            StartCoroutine(SpinCoroutine());
        }
    }

    IEnumerator SpinCoroutine()
    {
        HideAllPanels(); // Önceki açýlan paneli kapat

        float randomAngle = 360 * Random.Range(3, 6) + (22.5f * Random.Range(0, 16)); // 3-5 tur + rastgele bölge
        float currentAngle = wheel.eulerAngles.z;
        float targetAngle = currentAngle - randomAngle; // Saat yönünün tersine döndürme

        float duration = 3f; // Döndürme süresi
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float smoothT = Mathf.SmoothStep(0, 1, t);
            wheel.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(currentAngle, targetAngle, smoothT));
            yield return null;
        }

        wheel.rotation = Quaternion.Euler(0, 0, targetAngle); // Son açýyý ayarla

        yield return new WaitForSeconds(1f); // Biraz beklet

        Debug.Log("Çark Döndü! Açýlacak panel seçiliyor...");

        if (openedPanels.Count == panels.Length - 1) // 15. panel açýlacaksa
        {
            OpenLastPanel();
        }
        else
        {
            OpenRandomPanel();
        }

        isSpinning = false;
    }

    void OpenRandomPanel()
    {
        int randomPanelIndex;
        do
        {
            randomPanelIndex = Random.Range(0, panels.Length);
        }
        while (openedPanels.Contains(randomPanelIndex)); // Daha önce açýlmýþsa tekrar seç

        openedPanels.Add(randomPanelIndex); // Seçilen paneli listeye ekle
        panels[randomPanelIndex].SetActive(true); // Seçilen paneli aç

        Debug.Log("Rastgele Panel Açýldý: " + randomPanelIndex);
    }

    void OpenLastPanel()
    {
        do
        {
            lastPanelIndex = Random.Range(0, panels.Length);
        }
        while (openedPanels.Contains(lastPanelIndex));

        openedPanels.Add(lastPanelIndex);
        panels[lastPanelIndex].SetActive(true);

        Debug.Log("15. Panel Açýldý: " + lastPanelIndex);

        // Paneldeki panelkapat scriptini bul
        panelkapat kapatScript = panels[lastPanelIndex].GetComponentInChildren<panelkapat>();

        if (kapatScript != null)
        {
            // Özel bir event handler ekle
            panelkapat.OnPanelClosed += OnLastPanelClosedViaEvent;
            Debug.Log("15. Panel için kapatma eventi eklendi!");
        }
        else
        {
            Debug.LogError("15. Panelde panelkapat scripti bulunamadý!");
        }
    }

    void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); // Tüm panelleri kapat
        }
        endPanel.SetActive(false); // Bitiþ panelini de baþta kapat
        Debug.Log("Tüm paneller kapatýldý.");
    }

    private void OnLastPanelClosedViaEvent()
    {
        Debug.Log("Panel kapatma eventi tetiklendi!");

        // Event'i bir kere kullandýktan sonra kaldýr
        panelkapat.OnPanelClosed -= OnLastPanelClosedViaEvent;

        // Son panel kapandýysa bitiþ panelini aç
        if (lastPanelIndex != -1)
        {
            StartCoroutine(OpenEndPanelWithDelay());
        }
    }

    IEnumerator OpenEndPanelWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        OpenEndPanel();
    }

    void OpenEndPanel()
    {
        Debug.Log("Bitiþ Paneli Açýlýyor...");
        HideAllPanels(); // Önce diðer panelleri kapat
        endPanel.SetActive(true); // Oyun bitiþ panelini aç
    }
}