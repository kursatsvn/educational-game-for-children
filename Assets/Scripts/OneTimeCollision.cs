using UnityEngine;

public class OneTimeCollision : MonoBehaviour
{
    private bool hasCollided = false; // Çarpýþmanýn gerçekleþip gerçekleþmediðini kontrol eden flag

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer çarpýþma daha önce yapýlmadýysa
        if (!hasCollided)
        {
            Debug.Log("Çarpýþma gerçekleþti: " + other.gameObject.name);
            hasCollided = true;

            // Çarpýþma sonrasý yapýlacak iþlemleri burada gerçekleþtirebilirsiniz
        }
    }
}
