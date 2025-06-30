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
        // Baþlangýç pozisyonundan açýyý hesaplýyoruz
        Vector2 offset = (Vector2)transform.position - (Vector2)sun.position;

        // Elips denklemi için açýyý ters hesaplýyoruz
        angle = Mathf.Atan2(offset.y / radiusY, offset.x / radiusX) * Mathf.Rad2Deg;

        // Açýyý 0-360 derece arasýnda tutuyoruz
        if (angle < 0) angle += 360f;
    }

    void Update()
    {
        // Açýya zamanla artýþ ekliyoruz
        angle += orbitSpeed * Time.deltaTime;
        if (angle >= 360f) angle -= 360f;

        // Elips hareketi için X ve Y koordinatlarýný hesaplýyoruz
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radiusX;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radiusY;

        // Pozisyonu güncelliyoruz, Güneþin merkezini de ekleyerek
        transform.position = new Vector2(x, y) + (Vector2)sun.position;
    }
}
