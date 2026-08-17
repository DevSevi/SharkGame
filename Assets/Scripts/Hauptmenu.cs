using UnityEngine;
using UnityEngine.SceneManagement; // Wichtig für den Szenenwechsel

public class Hauptmenu : MonoBehaviour
{
    public void SpielStarten()
    {
        // Lädt die Szene mit dem Namen "Game". 
        // WICHTIG: Der Name muss exakt mit deiner Spielszene übereinstimmen!
        SceneManager.LoadScene("GameScene");
    }

    public void SpielBeenden()
    {
        // Schließt die App (funktioniert auf dem Android-Handy)
        Application.Quit();
        Debug.Log("Spiel wurde beendet.");
    }
}