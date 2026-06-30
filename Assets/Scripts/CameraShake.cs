using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        // Lấy vị trí gốc TƯƠNG ĐỐI so với Camera Container (thường là 0,0,0 hoặc vị trí offset ban đầu)
        Vector3 originalLocalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float damper = 1.0f - (elapsed / duration);

            // Tạo một điểm ngẫu nhiên trong vòng tròn bán kính = 1 rồi nhân với biên độ
            Vector3 randomPoint = Random.insideUnitSphere * magnitude * damper;

            // Giữ nguyên khoảng cách chiều sâu (thường là trục Z hoặc trục Y tùy góc nhìn toàn cảnh của bạn)
            // Ở đây ta chỉ làm lệch một chút X và Y để không làm mất góc nhìn toàn cảnh
            transform.localPosition = new Vector3(
                originalLocalPosition.x + randomPoint.x,
                originalLocalPosition.y + randomPoint.y,
                originalLocalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Trả về vị trí local ban đầu bên trong Container
        transform.localPosition = originalLocalPosition;
    }
}
