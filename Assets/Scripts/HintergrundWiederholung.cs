using UnityEngine;

public class HintergrundWiederholung : MonoBehaviour
{
    private Transform kameraTransform;
    private float bildBreite;

    void Start()
    {
        // Wir orientieren uns an der Kamera
        if (Camera.main != null)
        {
            kameraTransform = Camera.main.transform;
        }

        // Holt sich die exakte Breite des Sprites aus dem SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            bildBreite = spriteRenderer.bounds.size.x;
        }
    }

    void Update()
    {
        // Wenn die Kamera an der Mitte dieses Hintergrundbildes vorbeigeschwommen ist...
        if (kameraTransform != null && kameraTransform.position.x > transform.position.x + bildBreite)
        {
            // ...schiebe dieses Bild um die doppelte Breite nach rechts (vor das andere Bild)
            Vector3 neuePosition = new Vector3((transform.position.x + 2 * bildBreite), transform.position.y, transform.position.z);
            transform.position = neuePosition;
        }
    }
}