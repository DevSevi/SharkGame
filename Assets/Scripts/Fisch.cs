using System;
using UnityEngine;

public class Fisch : MonoBehaviour
{
    private Transform kameraTransform;

    void Start()
    {
        // Finde die Hauptkamera in der Szene
        if (Camera.main != null)
        {
            kameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Wenn der Fisch zu weit links von der Kamera ist (z.B. 15 Einheiten dahinter)
        if (kameraTransform != null && transform.position.x < kameraTransform.position.x - 15f)
        {
            Debug.Log("Fisch wird gelöscht");
            Destroy(transform.root.gameObject); // Fisch löschen, um Speicher zu sparen
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Prüfen, ob der Hai den Fisch berührt hat
        if (other.CompareTag("Player") || other.GetComponent<HaiController>() != null)
        {
            // Den GameManager in der Szene suchen und Punkte erhöhen
            GameManager gm = FindAnyObjectByType<GameManager>();
            if (gm != null)
            {
                gm.ErhöhePunkte();
            }

            // Fisch zerstören
            Destroy(transform.root.gameObject);
        }
    }
}