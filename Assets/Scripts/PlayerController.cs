using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Oyuncunun hareket h�z�n� belirler
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        // Rigidbody2D bile�enini al�yoruz
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Burada, y�n tu�lar�ndan gelen komutlarla hareket etme i�lemi yap�lmaz,
        // sadece tu�lar�n tetiklenmesi i�in bir mekanizma var.
    }

    // Hareket y�n�n� de�i�tirme fonksiyonu
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
