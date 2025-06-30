using UnityEngine;

public class SunPulsing : MonoBehaviour
{
    public float minScale = 0.5f;  // En küçük boyut
    public float maxScale = 2f;    // En büyük boyut
    public float speed = 1f;       // Büyüme hýzý

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;  // Baþlangýç boyutunu kaydet
    }

    void Update()
    {
        float scale = Mathf.PingPong(Time.time * speed, maxScale - minScale) + minScale;
        transform.localScale = originalScale * scale;
    }
}
