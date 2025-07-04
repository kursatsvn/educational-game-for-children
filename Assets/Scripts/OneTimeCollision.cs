using UnityEngine;

public class OneTimeCollision : MonoBehaviour
{
    private bool hasCollided = false; // Çarpışmanın gerçekleşip gerçekleşmediğini kontrol eden flag

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer çarpışma daha önce yapılmadıysa
        if (!hasCollided)
        {
            Debug.Log("Çarpışma gerçekleşti: " + other.gameObject.name);
            hasCollided = true;

            // Çarpışma sonrası yapılacak işlemleri burada gerçekleştirebilirsiniz
        }
    }
}
