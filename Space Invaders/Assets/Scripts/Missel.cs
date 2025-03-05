using UnityEngine;

public class Missel : MonoBehaviour
{
    public float speed = 3f;
    public float lifeTime = 5.0f;

    void Start()
    {
        // Destruir o míssil após o tempo de vida
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Invader"))
        {
            Destroy(other.gameObject); // Destroi o invasor
        }
        Destroy(gameObject); // Destroi o míssil
    }
}
