using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform sun;
    public float orbitSpeed = 10f;
    public float radiusX = 5f;
    public float radiusY = 3f;

    private float angle;

    void Start()
    {
        // Ba�lang�� pozisyonundan a��y� hesapl�yoruz
        Vector2 offset = (Vector2)transform.position - (Vector2)sun.position;

        // Elips denklemi i�in a��y� ters hesapl�yoruz
        angle = Mathf.Atan2(offset.y / radiusY, offset.x / radiusX) * Mathf.Rad2Deg;

        // A��y� 0-360 derece aras�nda tutuyoruz
        if (angle < 0) angle += 360f;
    }

    void Update()
    {
        // A��ya zamanla art�� ekliyoruz
        angle += orbitSpeed * Time.deltaTime;
        if (angle >= 360f) angle -= 360f;

        // Elips hareketi i�in X ve Y koordinatlar�n� hesapl�yoruz
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radiusX;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radiusY;

        // Pozisyonu g�ncelliyoruz, G�ne�in merkezini de ekleyerek
        transform.position = new Vector2(x, y) + (Vector2)sun.position;
    }
}
