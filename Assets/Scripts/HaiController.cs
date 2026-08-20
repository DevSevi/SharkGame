using UnityEngine;

public class HaiController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float aufwaertsKraft = 5f;
    public float vorwaertsGeschwindigkeit = 2f;
    [Header("Haptisches Feedback")]
    public bool vibrationAktiviert = true;

    [Header("Audio")]
    public AudioSource audioSource; // Der Lautsprecher auf dem Hai
    public AudioClip fressenSound;  // Die Sounddatei
    
    [Header("Visuelle Effekte")]
    // Referenz auf das Partikel-Prefab, das wir gerade erstellt haben
    public GameObject partikelPrefab;
    
    void Start()
    {
        // Falls der Rigidbody nicht im Inspector zugewiesen wurde, holen wir ihn uns hier
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        // Falls die AudioSource nicht im Inspector zugewiesen wurde, holen wir sie uns automatisch
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Konstante Vorwärtsbewegung
        rb.linearVelocity = new Vector2(vorwaertsGeschwindigkeit, rb.linearVelocity.y);
    }

    // Diese Funktion rufen wir später mit dem Button auf
    public void SchwimmeNachOben()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, aufwaertsKraft);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Fisch>() != null)
        {
            if (vibrationAktiviert)
            {
                // Handheld.Vibrate() löst die Standard-Vibration des Smartphones aus.
                // Auf Android vibriert das Handy typischerweise für eine kurze Zeitspanne (ca. 100-200ms).
                Handheld.Vibrate();
            }
            // PARTIKEL-SYSTEM AUSLÖSEN:
            if (partikelPrefab != null)
            {
                // Erzeugt das Partikel-System exakt an der Position des Fisches
                // Quaternion.identity bedeutet: keine Drehung
                Instantiate(partikelPrefab, other.transform.position, Quaternion.identity);
            }
            
            // Sound abspielen, falls beides zugewiesen ist
            if (audioSource != null && fressenSound != null)
            {
                // PlayOneShot sorgt dafür, dass der Sound komplett abspielt, 
                // selbst wenn der Hai kurz danach einen zweiten Fisch frisst.
                audioSource.PlayOneShot(fressenSound);
            }
        }
        
        // Wenn der Hai ein Objekt mit dem Namen "TodesZoneBoden" oder "TodesZoneDecke" berührt
        if (other.gameObject.name == "TodesZoneBoden" || other.gameObject.name == "TodesZoneDecke")
        {
            // Finde den GameManager in der Szene und löse das Game Over aus
            GameManager gm = FindAnyObjectByType<GameManager>();
            if (gm != null)
            {
                gm.GameOver();
            }

            // Optional: Schalte die Bewegung des Hais ab, damit er regungslos abstürzt
            vorwaertsGeschwindigkeit = 0;
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false; // Schaltet die Physik für den Hai ab
        }

        if (other.gameObject.tag == "EnemyShark")
        {
            if (fressenSound != null)
            {
                Vector3 soundPosition = new Vector3(transform.position.x, transform.position.y, -10f);
                AudioSource.PlayClipAtPoint(fressenSound, soundPosition, 2.0f);
            }

            // PARTIKEL-SYSTEM AUSLÖSEN:
            if (partikelPrefab != null)
            {
                // Erzeugt das Partikel-System exakt an der Position des Fisches
                // Quaternion.identity bedeutet: keine Drehung
                Instantiate(partikelPrefab, transform.position, Quaternion.identity);
            }
            // Player unsichtbar machen
            transform.root.gameObject.SetActive(false);
            
            // Finde den GameManager in der Szene und löse das Game Over aus
            GameManager gm = FindAnyObjectByType<GameManager>();
            if (gm != null)
            {
                gm.GameOver();
            }

            // Optional: Schalte die Bewegung des Hais ab, damit er regungslos abstürzt
            vorwaertsGeschwindigkeit = 0;
            rb.linearVelocity = Vector2.zero;
            rb.simulated = true;
        }
    }
}