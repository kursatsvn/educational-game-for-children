using UnityEngine;
using UnityEngine.UI;

public class ScratchMask : MonoBehaviour
{
    public Texture2D maskTexture;
    public RawImage rawImage;
    public float eraseRadius = 20f;
    public float clearThreshold = 0.5f; // %50'si silindiðinde tamamen yok olacak

    private Texture2D drawTexture;
    private int totalPixels;
    private int erasedPixels;
    private Vector2 lastPosition;
    private bool isErasing = false;
    private Color32[] pixels;
    private bool needsUpdate = false;
    private float updateInterval = 0.1f; // Her 100ms'de bir texture'ý güncelle
    private float nextUpdateTime = 0f;

    void Start()
    {
        drawTexture = new Texture2D(maskTexture.width, maskTexture.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(maskTexture, drawTexture);
        rawImage.texture = drawTexture;

        totalPixels = maskTexture.width * maskTexture.height;
        erasedPixels = 0;
        pixels = drawTexture.GetPixels32();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isErasing = true;
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rawImage.rectTransform, Input.mousePosition, null, out localPoint))
            {
                lastPosition = localPoint;
                EraseAt(localPoint);
            }
        }
        else if (Input.GetMouseButton(0) && isErasing)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rawImage.rectTransform, Input.mousePosition, null, out localPoint))
            {
                EraseLine(lastPosition, localPoint);
                lastPosition = localPoint;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isErasing = false;
        }

        // Belirli aralýklarla texture'ý güncelle
        if (needsUpdate && Time.time >= nextUpdateTime)
        {
            drawTexture.SetPixels32(pixels);
            drawTexture.Apply();
            needsUpdate = false;
            nextUpdateTime = Time.time + updateInterval;

            if ((float)erasedPixels / totalPixels > clearThreshold)
            {
                rawImage.gameObject.SetActive(false);
            }
        }
    }

    void EraseAt(Vector2 position)
    {
        int x = (int)((position.x + rawImage.rectTransform.rect.width / 2) / rawImage.rectTransform.rect.width * drawTexture.width);
        int y = (int)((position.y + rawImage.rectTransform.rect.height / 2) / rawImage.rectTransform.rect.height * drawTexture.height);

        int radius = Mathf.RoundToInt(eraseRadius);
        int startX = Mathf.Max(0, x - radius);
        int endX = Mathf.Min(drawTexture.width - 1, x + radius);
        int startY = Mathf.Max(0, y - radius);
        int endY = Mathf.Min(drawTexture.height - 1, y + radius);

        float radiusSqr = eraseRadius * eraseRadius;

        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                float distSqr = (i - x) * (i - x) + (j - y) * (j - y);
                if (distSqr <= radiusSqr)
                {
                    int index = j * drawTexture.width + i;
                    if (index < pixels.Length && pixels[index].a > 0)
                    {
                        erasedPixels++;
                        pixels[index].a = 0;
                    }
                }
            }
        }
        needsUpdate = true;
    }

    void EraseLine(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);
        int steps = Mathf.Max(10, Mathf.CeilToInt(distance / (eraseRadius * 0.5f)));

        for (int i = 0; i <= steps; i++)
        {
            Vector2 point = Vector2.Lerp(start, end, (float)i / steps);
            EraseAt(point);
        }
    }

    void OnDestroy()
    {
        if (drawTexture != null)
        {
            Destroy(drawTexture);
        }
    }
}