using UnityEngine;

public class ButonSes : MonoBehaviour
{
    public AudioSource sesKaynak; // Ses kaynaðý (AudioSource)

    public void SesCal()
    {
        sesKaynak.Play(); // Sesi çal
    }
}
