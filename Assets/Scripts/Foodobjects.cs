using UnityEngine;
using UnityEngine.UI;

public class FoodObject : MonoBehaviour
{
    public bool isCorrect; // Bu besin do�ru mu?
    private GameManager gameManager;
    private AudioSource audioSource;
    public AudioClip correctSound; // Do�ru temasta �al�nacak ses
    public AudioClip wrongSound; // Yanl�� temasta �al�nacak ses

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