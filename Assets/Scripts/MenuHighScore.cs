using UnityEngine;
using TMPro; // Wichtig für TextMeshPro

public class MenuHighScore : MonoBehaviour
{
    public TextMeshProUGUI menuHighscoreText;

    void Start()
    {
        // Holt den gespeicherten Wert (Standard ist 0, falls noch keiner existiert)
        int highscore = PlayerPrefs.GetInt("Highscore", 0);

        if (menuHighscoreText != null)
        {
            menuHighscoreText.text = "Highscore: " + highscore.ToString();
        }
    }
}