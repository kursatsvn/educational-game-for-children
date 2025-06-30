using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Image> titleImages; // Rastgele seçilecek baþlýklar (UI Image)
    public List<TitleToFoodMapping> titleToFoodMappings; // Baþlýða göre aktif olacak besinler (UI Image)
    public GameObject endPanel; // 6. baþlýktan sonra açýlacak panel
    private Image currentTitle; // Þu anki baþlýk
    private Dictionary<string, List<Image>> titleToFoodImages; // Baþlýða göre aktif olacak besinler (UI Image)
    private List<int> usedTitleIndices = new List<int>(); // Kullanýlmýþ baþlýk indeksleri
    private int titleCount = 0; // Kaç baþlýk seçildi

    void Start()
    {
        // Dictionary'yi doldur
        titleToFoodImages = new Dictionary<string, List<Image>>();
        foreach (var mapping in titleToFoodMappings)
        {
            titleToFoodImages[mapping.title] = mapping.foodImages;
        }

        // Baþlangýçta rastgele bir baþlýk seç
        SelectRandomTitle();
    }

    void SelectRandomTitle()
    {
        // Eðer 6 baþlýk seçildiyse paneli aç
        if (titleCount >= 12)
        {
            endPanel.SetActive(true);
            return;
        }

        // Önceki baþlýðý deaktif et
        if (currentTitle != null)
        {
            currentTitle.gameObject.SetActive(false);
        }

        // Rastgele bir baþlýk seç ve tekrar etmemesi için kontrol et
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, titleImages.Count);
        } while (usedTitleIndices.Contains(randomIndex));

        usedTitleIndices.Add(randomIndex); // Seçilen baþlýk indeksini ekle
        currentTitle = titleImages[randomIndex];
        currentTitle.gameObject.SetActive(true);
        titleCount++;

        // Baþlýða göre besinleri aktif et
        foreach (var pair in titleToFoodImages)
        {
            foreach (var obj in pair.Value)
            {
                obj.gameObject.SetActive(false);
            }
        }

        foreach (var obj in titleToFoodImages[currentTitle.name])
        {
            obj.gameObject.SetActive(true);
        }
    }

    public void SelectNewTitle()
    {
        SelectRandomTitle();
    }
}