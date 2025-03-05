using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject misselPrefab;
    public Transform firePoint;
    public float speed = 6.0f;
    public float fireRate = 0.4f; // Tempo de recarga em segundos
    public int lives = 3; // Número de vidas do jogador

    private float nextFireTime = 0f;

    void Update()
    {
        // Movimentação do jogador
        float move = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * move * speed * Time.deltaTime;

        // Disparo do missel
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Instantiate(misselPrefab, firePoint.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InvaderMissel"))
        {
            // Reduzir vidas do jogador
            lives--;
            Debug.Log("Jogador foi atingido! Vidas restantes: " + lives);

            // Verificar se o jogador perdeu todas as vidas
            if (lives <= 0)
            {
                Debug.Log("Jogador perdeu todas as vidas! Fim de jogo.");
                // Adicione aqui a lógica para finalizar o jogo, como reiniciar a cena ou mostrar uma tela de game over
                Destroy(gameObject);
            }

            // Destruir o míssil do invasor
            Destroy(other.gameObject);
        }
    }
}
