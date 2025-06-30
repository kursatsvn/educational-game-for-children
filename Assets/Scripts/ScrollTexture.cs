using UnityEngine;
using UnityEngine.UI;

public class ScrollTexture : MonoBehaviour
{
    public float speed = 0.5f;  // Kayma h�z�
    private RawImage rawImage;  // RawImage bile�eni

    private float offset = 0f;  // Kayma i�in ba�lang�� offseti

    void Start()
    {
        // RawImage bile�enini al�yoruz
        rawImage = GetComponent<RawImage>();

        // E�er RawImage bile�eni yoksa bir hata verelim
        if (rawImage == null)
        {
            Debug.LogError("RawImage bile�eni bulunamad�!");
        }
    }

    void Update()
    {
        if (rawImage != null)
        {
            // Zamanla offseti sa�a kayd�r�yoruz
            offset -= Time.deltaTime * speed;  // H�zl� kayma i�in deltaTime ile �arp�yoruz

            // Offseti d�ng�sel hale getirmek i�in 0 ile 1 aras�nda s�n�rl�yoruz
            offset = Mathf.Repeat(offset, 1f);

            // Materyalin dokusunun offset de�erini g�ncelliyoruz
            rawImage.uvRect = new Rect(offset, 0, 1, 1);
        }
    }
}
