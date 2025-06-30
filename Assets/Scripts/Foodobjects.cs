using UnityEngine;
using UnityEngine.UI;

public class FoodObject : MonoBehaviour
{
    public bool isCorrect; // Bu besin doðru mu?
    private GameManager gameManager;
    private AudioSource audioSource;
    public AudioClip correctSound; // Doðru temasta çalýnacak ses
    public AudioClip wrongSound; // Yanlýþ temasta çalýnacak ses

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = gameManager.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isCorrect)
            {
                audioSource.PlayOneShot(correctSound);
                gameManager.SelectNewTitle();
            }
            else
            {
                audioSource.PlayOneShot(wrongSound);
            }
        }
    }
}