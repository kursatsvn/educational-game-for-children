using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cıkıs : MonoBehaviour
{
    public GameObject soruPaneli; // UI Panelini buraya sürükle

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player nesnesinin tag'ı "Player" olmalı
        {
            soruPaneli.SetActive(true); // Paneli aç
            //Time.timeScale = 0f; // Oyunu durdur (isteğe bağlı)
        }
    }
}
