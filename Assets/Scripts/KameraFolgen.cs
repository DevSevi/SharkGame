using UnityEngine;

public class KameraFolgen : MonoBehaviour
{
    public Transform haiTransform;
    public float abstandX = 5f; // Wie weit die Kamera hinter/vor dem Hai ist

    void LateUpdate()
    {
        if (haiTransform != null)
        {
            // Die Kamera folgt nur der X-Position des Hais
            transform.position = new Vector3(haiTransform.position.x + abstandX, transform.position.y, transform.position.z);
        }
    }
}