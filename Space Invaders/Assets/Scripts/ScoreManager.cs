using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        // Garantir que apenas uma instância do ScoreManager exista
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
        Debug.Log("Pontuação adicionada: " + points + ", Pontuação total: " + score);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            Debug.Log("Texto da pontuação atualizado: " + scoreText.text);
        }
        else
        {
            Debug.LogWarning("scoreText não está atribuído no ScoreManager.");
        }
    }
}
