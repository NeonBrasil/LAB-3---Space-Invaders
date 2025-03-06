using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    void Update()
    {
        // Verifica se qualquer botão foi pressionado
        if (Input.anyKeyDown)
        {
            // Reinicia a cena do jogo (substitua "GameScene" pelo nome da sua cena do jogo)
            SceneManager.LoadScene("SampleScene");
        }
    }
}

