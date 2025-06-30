using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Image> titleImages; // Rastgele se�ilecek ba�l�klar (UI Image)
    public List<TitleToFoodMapping> titleToFoodMappings; // Ba�l��a g�re aktif olacak besinler (UI Image)
    public GameObject endPanel; // 6. ba�l�ktan sonra a��lacak panel
    private Image currentTitle; // �u anki ba�l�k
    private Dictionary<string, List<Image>> titleToFoodImages; // Ba�l��a g�re aktif olacak besinler (UI Image)
    private List<int> usedTitleIndices = new List<int>(); // Kullan�lm�� ba�l�k indeksleri
    private int titleCount = 0; // Ka� ba�l�k se�ildi

    void Start()
    {
        // Dictionary'yi doldur
        titleToFoodImages = new Dictionary<string, List<Image>>();
        foreach (var mapping in titleToFoodMappings)
        {
            titleToFoodImages[mapping.title] = mapping.foodImages;
        }

        // Ba�lang��ta rastgele bir ba�l�k se�
        SelectRandomTitle();
    }

    void SelectRandomTitle()
    {
        // E�er 6 ba�l�k se�ildiyse paneli a�
        if (titleCount >= 12)
        {
            endPanel.SetActive(true);
            return;
        }

        // �nceki ba�l��� deaktif et
        if (currentTitle != null)
        {
            currentTitle.gameObject.SetActive(false);
        }

        // Rastgele bir ba�l�k se� ve tekrar etmemesi i�in kontrol et
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, titleImages.Count);
        } while (usedTitleIndices.Contains(randomIndex));

        usedTitleIndices.Add(randomIndex); // Se�ilen ba�l�k indeksini ekle
        currentTitle = titleImages[randomIndex];
        currentTitle.gameObject.SetActive(true);
        titleCount++;

        // Ba�l��a g�re besinleri aktif et
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