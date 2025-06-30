using UnityEngine;

public class SunPulsing : MonoBehaviour
{
    public float minScale = 0.5f;  // En k���k boyut
    public float maxScale = 2f;    // En b�y�k boyut
    public float speed = 1f;       // B�y�me h�z�

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;  // Ba�lang�� boyutunu kaydet
    }

    void Update()
    {
        float scale = Mathf.PingPong(Time.time * speed, maxScale - minScale) + minScale;
        transform.localScale = originalScale * scale;
    }
}
