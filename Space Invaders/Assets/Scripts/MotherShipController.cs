using UnityEngine;

public class MotherShipController : MonoBehaviour
{
    public float speed = 5.0f; // Velocidade de movimento da nave m�e

    void Update()
    {
        // Movimento horizontal da nave m�e da direita para a esquerda
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Verifica se a nave m�e saiu da tela pela esquerda
        if (transform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            // Destruir a nave m�e ao sair da tela pela esquerda
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missel"))
        {
            // Destruir a nave m�e e o m�ssil ao colidirem
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}



