using UnityEngine;
using UnityEngine.UI;

public class ScrollTexture : MonoBehaviour
{
    public float speed = 0.5f;  // Kayma hýzý
    private RawImage rawImage;  // RawImage bileþeni

    private float offset = 0f;  // Kayma için baþlangýç offseti

    void Start()
    {
        // RawImage bileþenini alýyoruz
        rawImage = GetComponent<RawImage>();

        // Eðer RawImage bileþeni yoksa bir hata verelim
        if (rawImage == null)
        {
            Debug.LogError("RawImage bileþeni bulunamadý!");
        }
    }

    void Update()
    {
        if (rawImage != null)
        {
            // Zamanla offseti saða kaydýrýyoruz
            offset -= Time.deltaTime * speed;  // Hýzlý kayma için deltaTime ile çarpýyoruz

            // Offseti döngüsel hale getirmek için 0 ile 1 arasýnda sýnýrlýyoruz
            offset = Mathf.Repeat(offset, 1f);

            // Materyalin dokusunun offset deðerini güncelliyoruz
            rawImage.uvRect = new Rect(offset, 0, 1, 1);
        }
    }
}
