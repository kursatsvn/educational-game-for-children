using UnityEngine;

public class OneTimeCollision : MonoBehaviour
{
    private bool hasCollided = false; // �arp��man�n ger�ekle�ip ger�ekle�medi�ini kontrol eden flag

    void OnTriggerEnter2D(Collider2D other)
    {
        // E�er �arp��ma daha �nce yap�lmad�ysa
        if (!hasCollided)
        {
            Debug.Log("�arp��ma ger�ekle�ti: " + other.gameObject.name);
            hasCollided = true;

            // �arp��ma sonras� yap�lacak i�lemleri burada ger�ekle�tirebilirsiniz
        }
    }
}
