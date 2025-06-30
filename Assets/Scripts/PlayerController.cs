using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Oyuncunun hareket hýzýný belirler
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        // Rigidbody2D bileþenini alýyoruz
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Burada, yön tuþlarýndan gelen komutlarla hareket etme iþlemi yapýlmaz,
        // sadece tuþlarýn tetiklenmesi için bir mekanizma var.
    }

    // Hareket yönünü deðiþtirme fonksiyonu
    public void SetMovementDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    void FixedUpdate()
    {
        // Oyuncuyu sabit zaman dilimlerinde hareket ettiriyoruz
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

}
