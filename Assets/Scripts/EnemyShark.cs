using UnityEngine;

public class EnemyShark : MonoBehaviour
{
    public float geschwindigkeit = 4f;
    private Transform _kameraTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            _kameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Schwimmt kontinuierlich nach links (entgegengesetzte Richtung)
        transform.position += (Vector3.left * (geschwindigkeit * Time.deltaTime));

        // Automatische Speicherbereinigung, wenn er hinter der Kamera verschwindet
        if (_kameraTransform != null && transform.position.x < _kameraTransform.position.x - 15f)
        {
            Destroy(gameObject);
        }
    }
}