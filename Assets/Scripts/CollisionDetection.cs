using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private ArrowController arrowController; // Ok hareketini kontrol eden script
    private AudioSource audioSource; // Sesleri çalacak olan AudioSource

    public AudioClip correctSound; // Doðru ses dosyasý
    public AudioClip incorrectSound; // Yanlýþ ses dosyasý

    void Start()
    {
        // Ok objesini bul ve ArrowController scriptini al
        arrowController = GameObject.FindGameObjectWithTag("Arrow").GetComponent<ArrowController>();

        // SoundManager objesini bul ve AudioSource bileþenini al
        GameObject soundManager = GameObject.Find("SoundManager"); // SoundManager'ýn ismi burada 'SoundManager' olmalý
        if (soundManager != null)
        {
            audioSource = soundManager.GetComponent<AudioSource>(); // SoundManager'dan AudioSource al
        }
        else
        {
            Debug.LogError("SoundManager GameObject bulunamadý. Lütfen sahnede bir SoundManager oluþturun.");
        }

        if (arrowController == null)
        {
            Debug.LogError("ArrowController referansý bulunamadý. Lütfen ok objesine ArrowController ekleyin.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Oyuncu ile çarpýþma algýlandý
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Oyuncu ile çarpýþma algýlandý!");

            // Besinin türünü kontrol et
            Food food = gameObject.GetComponent<Food>();

            if (food != null) // Eðer Food scripti varsa
            {
                if (food.foodType == FoodType.Correct) // Doðru besin
                {
                    Debug.Log("Doðru besin çarpýþtý!");
                    if (arrowController != null) // arrowController referansý null deðilse
                    {
                        arrowController.MoveArrow(true); // Ok yukarý hareket eder
                    }

                    // Doðru ses çal
                    if (audioSource != null && correctSound != null)
                    {
                        audioSource.PlayOneShot(correctSound);
                    }
                }
                else if (food.foodType == FoodType.Incorrect) // Yanlýþ besin
                {
                    Debug.Log("Yanlýþ besin çarpýþtý!");
                    if (arrowController != null) // arrowController referansý null deðilse
                    {
                        arrowController.MoveArrow(false); // Ok aþaðý hareket eder
                    }

                    // Yanlýþ ses çal
                    if (audioSource != null && incorrectSound != null)
                    {
                        audioSource.PlayOneShot(incorrectSound);
                    }
                }
            }
            else
            {
                Debug.LogWarning("Food scripti bulunamadý. Bu nesneye Food scriptini ekleyin.");
            }

            // Besini yok et
            Destroy(gameObject);
        }
    }
}
