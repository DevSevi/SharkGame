using UnityEngine;

public class FischSpawner : MonoBehaviour
{
    [Header("Fisch-Vorlagem")]
    public GameObject[] fischPrefabs;

    [Header("Einstellungen")]
    public float spawnIntervall = 2f; // Alle 2 Sekunden ein neuer Fisch
    public float minY = -4f;          // Tiefste Position für einen Fisch
    public float maxY = 4f;           // Höchste Position für einen Fisch

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // Wenn die Zeit für den nächsten Spawn abgelaufen ist
        if (timer >= spawnIntervall)
        {
            SpawnFisch();
            timer = 0f; // Timer zurücksetzen
        }
    }

    void SpawnFisch()
    {
        // Sicherheitsabfrage: Falls du vergessen hast, Prefabs im Inspector zuzuweisen
        if (fischPrefabs == null || fischPrefabs.Length == 0)
        {
            Debug.LogWarning("Bitte füge dem FischSpawner im Inspector mindestens ein Fisch-Prefab hinzu!");
            return;
        }

        // 1. Zufälligen Index aus dem Array auswählen
        int zufaelligerIndex = Random.Range(0, fischPrefabs.Length);
        GameObject gewaehlterFisch = fischPrefabs[zufaelligerIndex];

        // 2. Zufällige Y-Position (Höhe) bestimmen
        float zufaelligesY = Random.Range(minY, maxY);
        
        // Die Position des Spawners nehmen, aber mit dem zufälligen Y-Wert kombinieren
        Vector3 spawnPosition = new Vector3(transform.position.x, zufaelligesY, 0f);

        // 3. Den zufällig ausgewählten Fisch in der Spielwelt erschaffen
        Instantiate(gewaehlterFisch, spawnPosition, Quaternion.identity);
    }
}