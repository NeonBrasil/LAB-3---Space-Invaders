using UnityEngine;

public class InvaderMissel : MonoBehaviour
{
    public float speed = 2f;
    public float lifeTime = 5.0f;

    void Start()
    {
        // Destruir o míssil após o tempo de vida
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        Vector3 oldPosition = transform.position;
        transform.position += Vector3.down * speed * Time.deltaTime;
        Vector3 newPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Invader")) // Não destrói os invasores
        {
            Destroy(gameObject); // Destrói ao colidir com algo que não seja um invasor
        }
    }
}
