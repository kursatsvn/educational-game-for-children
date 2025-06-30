using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    public float spawnInterval = 2f;
    private float timer;

    void Start()
    {
        timer = 0; // �lk besinin hemen spawn olmas� i�in 0 olarak ba�lat�yoruz
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnFood();
            timer = 0;
        }
    }

    void SpawnFood()
    {
        if (foodPrefabs.Length > 0)
        {
            int index = Random.Range(0, foodPrefabs.Length);
            GameObject food = Instantiate(foodPrefabs[index]);

            // Canvas i�inde g�stermek i�in ayarla
            food.transform.SetParent(GameObject.Find("Canvas").transform, false);

            // UI pozisyonu ayarla
            RectTransform rectTransform = food.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(Random.Range(-500f, 500f), Random.Range(400f, 600f));

            Debug.Log("Food spawned at: " + rectTransform.anchoredPosition);
        }
        else
        {
            Debug.LogError("Food Prefabs array is empty!");
        }
    }

}