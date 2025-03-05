using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        // Garantir que apenas uma inst�ncia do ScoreManager exista
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Pontua��o adicionada: " + points + ", Pontua��o total: " + score);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            Debug.Log("Texto da pontua��o atualizado: " + scoreText.text);
        }
        else
        {
            Debug.LogWarning("scoreText n�o est� atribu�do no ScoreManager.");
        }
    }
}
