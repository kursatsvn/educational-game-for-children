using UnityEngine;

public class ButonSes : MonoBehaviour
{
    public AudioSource sesKaynak; // Ses kayna�� (AudioSource)

    public void SesCal()
    {
        sesKaynak.Play(); // Sesi �al
    }
}
