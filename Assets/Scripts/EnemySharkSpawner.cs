using UnityEngine;

public class EnemySharkSpawner : MonoBehaviour
{
    [Header("EnemyShark Vorlage")]
    public GameObject sharkPrefab;

    [Header("Spawn-Zeiten")]
    public float startVerzoegerung = 60f; // Startet erst nach 60 Sekunden
    public float spawnIntervall = 5f;      // Danach alle 5 Sekunden

    [Header("Positionierung")]
    public float spawnAbstandRechts = 14f; // Weit genug rechts außerhalb des Sichtfelds
    public float minY = -3.5f;
    public float maxY = 3.5f;

    private float timer = 0f;
    private float spielZeit = 0f;

    void Update()
    {
        spielZeit += Time.deltaTime;

        // Erst starten, wenn die 60 Sekunden um sind
        if (spielZeit < startVerzoegerung)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnIntervall)
        {
            SpawnShark();
            timer = 0f;
        }
    }

    void SpawnShark()
    {
        if (sharkPrefab == null || Camera.main == null) return;

        // Spawnt rechts vor der sich bewegenden Kamera
        float spawnX = Camera.main.transform.position.x + spawnAbstandRechts;
        float zufaelligesY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, zufaelligesY, 0f);

        Instantiate(sharkPrefab, spawnPosition, sharkPrefab.transform.rotation);
    }
}