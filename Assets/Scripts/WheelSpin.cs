using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelSpin : MonoBehaviour
{
    public RectTransform wheel; // �ark�n RectTransform'u
    public Button spinButton;   // D�nme butonu
    public GameObject[] panels; // A��lacak 15 farkl� panel
    public GameObject endPanel; // Oyun biti� paneli

    private List<int> openedPanels = new List<int>(); // A��lm�� panellerin indeksleri
    private bool isSpinning = false;
    private int lastPanelIndex = -1; // 15. panelin indeksini saklayaca��z

    void Start()
    {
        spinButton.onClick.AddListener(SpinWheel);
        HideAllPanels();
        Debug.Log("Oyun Ba�lad�! T�m paneller gizlendi.");

        // Event'i temizle (e�er �nceden kalm��sa)
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
            Debug.Log("�ark D�nd�r�l�yor...");
            isSpinning = true;
            StartCoroutine(SpinCoroutine());
        }
    }

    IEnumerator SpinCoroutine()
    {
        HideAllPanels(); // �nceki a��lan paneli kapat

        float randomAngle = 360 * Random.Range(3, 6) + (22.5f * Random.Range(0, 16)); // 3-5 tur + rastgele b�lge
        float currentAngle = wheel.eulerAngles.z;
        float targetAngle = currentAngle - randomAngle; // Saat y�n�n�n tersine d�nd�rme

        float duration = 3f; // D�nd�rme s�resi
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float smoothT = Mathf.SmoothStep(0, 1, t);
            wheel.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(currentAngle, targetAngle, smoothT));
            yield return null;
        }

        wheel.rotation = Quaternion.Euler(0, 0, targetAngle); // Son a��y� ayarla

        yield return new WaitForSeconds(1f); // Biraz beklet

        Debug.Log("�ark D�nd�! A��lacak panel se�iliyor...");

        if (openedPanels.Count == panels.Length - 1) // 15. panel a��lacaksa
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
        while (openedPanels.Contains(randomPanelIndex)); // Daha �nce a��lm��sa tekrar se�

        openedPanels.Add(randomPanelIndex); // Se�ilen paneli listeye ekle
        panels[randomPanelIndex].SetActive(true); // Se�ilen paneli a�

        Debug.Log("Rastgele Panel A��ld�: " + randomPanelIndex);
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

        Debug.Log("15. Panel A��ld�: " + lastPanelIndex);

        // Paneldeki panelkapat scriptini bul
        panelkapat kapatScript = panels[lastPanelIndex].GetComponentInChildren<panelkapat>();

        if (kapatScript != null)
        {
            // �zel bir event handler ekle
            panelkapat.OnPanelClosed += OnLastPanelClosedViaEvent;
            Debug.Log("15. Panel i�in kapatma eventi eklendi!");
        }
        else
        {
            Debug.LogError("15. Panelde panelkapat scripti bulunamad�!");
        }
    }

    void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); // T�m panelleri kapat
        }
        endPanel.SetActive(false); // Biti� panelini de ba�ta kapat
        Debug.Log("T�m paneller kapat�ld�.");
    }

    private void OnLastPanelClosedViaEvent()
    {
        Debug.Log("Panel kapatma eventi tetiklendi!");

        // Event'i bir kere kulland�ktan sonra kald�r
        panelkapat.OnPanelClosed -= OnLastPanelClosedViaEvent;

        // Son panel kapand�ysa biti� panelini a�
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
        Debug.Log("Biti� Paneli A��l�yor...");
        HideAllPanels(); // �nce di�er panelleri kapat
        endPanel.SetActive(true); // Oyun biti� panelini a�
    }
}