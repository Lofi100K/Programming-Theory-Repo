using TMPro; // Per gestire il TextMeshPro
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Riferimento al componente TMP per il testo
    private int score; // Variabile per tenere traccia del punteggio

    void OnEnable()
    {
        // Iscrivi il metodo AddScore all'evento
        EnemyBase.OnEnemyDestroyed += AddScore;
    }

    void OnDisable()
    {
        // Rimuovi l'iscrizione per evitare errori quando l'oggetto viene distrutto
        EnemyBase.OnEnemyDestroyed -= AddScore;
    }

    // Metodo per aggiungere punti
    public void AddScore(int points)
    {
        score += points; // Incrementa il punteggio
        UpdateScoreText(); // Aggiorna il testo
    }

    // Metodo per aggiornare il testo sullo schermo
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Modifica il testo del TMP
    }
}
