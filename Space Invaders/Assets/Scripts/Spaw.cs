using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipSpawner : MonoBehaviour
{
    [Header("Configurações de Spawn")]
    [SerializeField] private GameObject[] enemyPrefabs;     // Array de prefabs de inimigos
    [SerializeField] private float minSpawnInterval = 2f;   // Intervalo mínimo entre spawns
    [SerializeField] private float maxSpawnInterval = 5f;   // Intervalo máximo entre spawns
    [SerializeField] private int maxEnemiesOnScreen = 10;   // Número máximo de inimigos na tela
    [SerializeField] private Transform[] spawnPoints;       // Pontos de spawn dentro da nave mãe

    [Header("Movimento da Nave Mãe")]
    [SerializeField] private float moveSpeed = 2f;          // Velocidade de movimento
    [SerializeField] private bool movesHorizontally = true; // Se move horizontalmente (ou verticalmente)
    [SerializeField] private float changeDirectionChance = 0.05f; // Chance de mudar de direção a cada frame
    [SerializeField] private float screenBorderOffset = 0.5f; // Distância da borda da tela

    // Controle interno
    private List<GameObject> activeEnemies = new List<GameObject>();
    private float nextSpawnTime;
    private Camera mainCamera;
    private int direction = 1; // 1 = direita/baixo, -1 = esquerda/cima

    void Start()
    {
        mainCamera = Camera.main;
        CalculateNextSpawnTime();
    }

    void Update()
    {
        // Movimento da nave mãe
        MoveMotherShip();

        // Checar se é hora de spawnar
        if (Time.time >= nextSpawnTime && activeEnemies.Count < maxEnemiesOnScreen)
        {
            SpawnEnemy();
            CalculateNextSpawnTime();
        }

        // Limpar inimigos destruídos da lista
        CleanupDestroyedEnemies();
    }

    private void MoveMotherShip()
    {
        // Chance aleatória de mudar de direção
        if (Random.value < changeDirectionChance * Time.deltaTime)
        {
            direction *= -1;
        }

        // Movimento horizontal ou vertical
        Vector3 movement;
        if (movesHorizontally)
        {
            movement = new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            movement = new Vector3(0, direction * moveSpeed * Time.deltaTime, 0);
        }

        // Aplicar movimento
        transform.position += movement;

        // Verificar limites da tela
        KeepInScreenBounds();
    }

    private void KeepInScreenBounds()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (movesHorizontally)
        {
            if (viewportPosition.x < screenBorderOffset / 100f)
            {
                viewportPosition.x = screenBorderOffset / 100f;
                direction = 1; // Mudar direção para direita
            }
            else if (viewportPosition.x > 1 - screenBorderOffset / 100f)
            {
                viewportPosition.x = 1 - screenBorderOffset / 100f;
                direction = -1; // Mudar direção para esquerda
            }
        }
        else
        {
            if (viewportPosition.y < screenBorderOffset / 100f)
            {
                viewportPosition.y = screenBorderOffset / 100f;
                direction = 1; // Mudar direção para cima
            }
            else if (viewportPosition.y > 1 - screenBorderOffset / 100f)
            {
                viewportPosition.y = 1 - screenBorderOffset / 100f;
                direction = -1; // Mudar direção para baixo
            }
        }

        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
    }

    private void SpawnEnemy()
    {
        // Escolher um inimigo aleatório do array
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        // Escolher um ponto de spawn
        Transform spawnPoint = spawnPoints.Length > 0
            ? spawnPoints[Random.Range(0, spawnPoints.Length)]
            : transform;

        // Criar o inimigo
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.identity);

        // Adicionar à lista de controle
        activeEnemies.Add(enemy);
    }

    private void CalculateNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void CleanupDestroyedEnemies()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }
    }

    // Método público para forçar o spawn de inimigos (pode ser chamado por outros scripts)
    public void ForceSpawn(int count = 1)
    {
        for (int i = 0; i < count && activeEnemies.Count < maxEnemiesOnScreen; i++)
        {
            SpawnEnemy();
        }
    }

    // Método opcional para visualização no editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Desenhar gizmos para os pontos de spawn
        if (spawnPoints != null)
        {
            foreach (Transform point in spawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawSphere(point.position, 0.2f);
                }
            }
        }
    }
}