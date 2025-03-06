using UnityEngine;

public class MotherShipController : MonoBehaviour
{
    public float speed = 5.0f; // Velocidade de movimento da nave mãe

    void Update()
    {
        // Movimento horizontal da nave mãe da direita para a esquerda
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Verifica se a nave mãe saiu da tela pela esquerda
        if (transform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            // Destruir a nave mãe ao sair da tela pela esquerda
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missel"))
        {
            // Destruir a nave mãe e o míssil ao colidirem
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}



