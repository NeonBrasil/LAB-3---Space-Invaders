using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int invaderCount;

    void Start()
    {
        // Contar o n�mero inicial de invasores
        invaderCount = GameObject.FindGameObjectsWithTag("Invader").Length;
    }

    public void InvaderDestroyed()
    {
        invaderCount--;
        Debug.Log("Invasores restantes: " + invaderCount);

        // Verificar se todos os invasores foram destru�dos
        if (invaderCount <= 0)
        {
            Debug.Log("Todos os invasores foram destru�dos! Voc� venceu!");
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
