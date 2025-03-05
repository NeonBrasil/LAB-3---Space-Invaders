using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject misselPrefab;
    public Transform firePoint;
    public float speed = 6.0f;
    public float fireRate = 0.4f; // Tempo de recarga em segundos

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
}
