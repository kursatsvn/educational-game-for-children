using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private ArrowController arrowController; // Ok hareketini kontrol eden script
    private AudioSource audioSource; // Sesleri �alacak olan AudioSource

    public AudioClip correctSound; // Do�ru ses dosyas�
    public AudioClip incorrectSound; // Yanl�� ses dosyas�

    void Start()
    {
        // Ok objesini bul ve ArrowController scriptini al
        arrowController = GameObject.FindGameObjectWithTag("Arrow").GetComponent<ArrowController>();

        // SoundManager objesini bul ve AudioSource bile�enini al
        GameObject soundManager = GameObject.Find("SoundManager"); // SoundManager'�n ismi burada 'SoundManager' olmal�
        if (soundManager != null)
        {
            audioSource = soundManager.GetComponent<AudioSource>(); // SoundManager'dan AudioSource al
        }
        else
        {
            Debug.LogError("SoundManager GameObject bulunamad�. L�tfen sahnede bir SoundManager olu�turun.");
        }

        if (arrowController == null)
        {
            Debug.LogError("ArrowController referans� bulunamad�. L�tfen ok objesine ArrowController ekleyin.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Oyuncu ile �arp��ma alg�land�
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Oyuncu ile �arp��ma alg�land�!");

            // Besinin t�r�n� kontrol et
            Food food = gameObject.GetComponent<Food>();

            if (food != null) // E�er Food scripti varsa
            {
                if (food.foodType == FoodType.Correct) // Do�ru besin
                {
                    Debug.Log("Do�ru besin �arp��t�!");
                    if (arrowController != null) // arrowController referans� null de�ilse
                    {
                        arrowController.MoveArrow(true); // Ok yukar� hareket eder
                    }

                    // Do�ru ses �al
                    if (audioSource != null && correctSound != null)
                    {
                        audioSource.PlayOneShot(correctSound);
                    }
                }
                else if (food.foodType == FoodType.Incorrect) // Yanl�� besin
                {
                    Debug.Log("Yanl�� besin �arp��t�!");
                    if (arrowController != null) // arrowController referans� null de�ilse
                    {
                        arrowController.MoveArrow(false); // Ok a�a�� hareket eder
                    }

                    // Yanl�� ses �al
                    if (audioSource != null && incorrectSound != null)
                    {
                        audioSource.PlayOneShot(incorrectSound);
                    }
                }
            }
            else
            {
                Debug.LogWarning("Food scripti bulunamad�. Bu nesneye Food scriptini ekleyin.");
            }

            // Besini yok et
            Destroy(gameObject);
        }
    }
}
