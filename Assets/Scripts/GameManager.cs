using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // WICHTIG: Erlaubt uns, das TextMeshPro-Textfeld zu steuern

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI punkteTextAnzeigefeld; // Verbindung zum UI-Text
    public TextMeshProUGUI timerTextAnzeigefeld;

    public GameObject gameOverObjekt;
    
    [Header("Timer Einstellungen")]
    // 2 Minuten entsprechen 120 Sekunden (2 * 60)
    public float verbleibendeZeit = 120f;
    
    [Header("Audio")]
    public AudioSource musikAudioSource;
    
    private int punkteStand = 0;
    private bool spielVorbei = false;

   
    void Start()
    {
        // Sicherstellen, dass das Spiel mit 0 Punkten startet
        PunkteAktualisieren();
    }
    
    void Update()
    {
        // Wenn das Spiel läuft, zählen wir die Zeit unaufhaltsam runter
        if (!spielVorbei)
        {
            if (verbleibendeZeit > 0)
            {
                // Zieht pro Sekunde genau 1 von der verbleibenden Zeit ab
                verbleibendeZeit -= Time.deltaTime;
                TimerTextAktualisieren();
            }
            else
            {
                // Zeit ist abgelaufen!
                verbleibendeZeit = 0;
                spielVorbei = true;
                GameOver();
            }
        }
    }


    // Diese Funktion wird vom Fisch aufgerufen, wenn er gefressen wird
    public void ErhöhePunkte()
    {
        if (!spielVorbei)
        {
            punkteStand += 1; // 1 Punkt dazu
            PunkteAktualisieren();
        }
    }

    void PunkteAktualisieren()
    {
        if (punkteTextAnzeigefeld != null)
        {
            punkteTextAnzeigefeld.text = $"Score: {punkteStand.ToString()}";
        }
    }
    
    // Wandelt die Sekunden in ein schönes "MM:SS" Format um
    void TimerTextAktualisieren()
    {
        if (timerTextAnzeigefeld != null)
        {
            // Berechne Minuten und Sekunden
            int minuten = Mathf.FloorToInt(verbleibendeZeit / 60);
            int sekunden = Mathf.FloorToInt(verbleibendeZeit % 60);

            // string.Format sorgt dafür, dass aus "9" Sekunden "09" angezeigt wird
            timerTextAnzeigefeld.text = string.Format("{0:00}:{1:00}", minuten, sekunden);
        }
    }

    public void GameOver()
    {
        spielVorbei = true;
        
        if (gameOverObjekt != null)
        {
            gameOverObjekt.SetActive(true);
        }

        if (timerTextAnzeigefeld != null)
        {
            timerTextAnzeigefeld.gameObject.SetActive(false);
        }
        
        // Highscore prüfen und speichern
        int alterHighscore = PlayerPrefs.GetInt("Highscore", 0);
        if (punkteStand > alterHighscore)
        {
            PlayerPrefs.SetInt("Highscore", punkteStand);
            PlayerPrefs.Save(); // Speichert den Wert permanent ab
        }
        
        if (musikAudioSource != null)
        {
            musikAudioSource.Stop();
        }
        
        Debug.Log("Game Over!");
        Invoke("WechsleInsMenu", 2.5f);
    }
    
    void WechsleInsMenu()
    {
        SceneManager.LoadScene("MenuScene"); 
    }
}