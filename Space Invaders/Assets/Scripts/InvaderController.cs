using UnityEngine;

public class InvaderController : MonoBehaviour
{
    public GameObject invaderMisselPrefab;
    public Transform firePoint;
    public float speed = 0.5f;
    public float moveDistance = 1.0f;
    public float moveDownDistance = 0.5f;
    public float fireRate = 2.0f; // Tempo de recarga em segundos
    public int invaderType = 1; // Tipo do invasor (1, 2 ou 3)

    private Vector3 targetPosition;
    private bool movingRight = true;
    private float nextFireTime = 0f;
    private GameManager gameManager;

    void Start()
    {
        targetPosition = transform.position;

        // Se firePoint não estiver atribuído, usa o próprio transform do Invader
        if (firePoint == null)
        {
            firePoint = transform;
        }

        // Obter referência ao GameManager
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Movimentação suave
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // Verifica se chegou ao destino
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            MoveInvader();
        }

        // Disparo do missel
        if (Time.time >= nextFireTime && IsFirstInRow())
        {
            Debug.Log("Invader atirando");
            Instantiate(invaderMisselPrefab, firePoint.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    void MoveInvader()
    {
        if (movingRight)
        {
            targetPosition += Vector3.right * moveDistance;
            if (targetPosition.x >= transform.position.x + moveDistance)
            {
                movingRight = false;
                targetPosition += Vector3.down * moveDownDistance;
            }
        }
        else
        {
            targetPosition += Vector3.left * moveDistance;
            if (targetPosition.x <= transform.position.x - moveDistance)
            {
                movingRight = true;
                targetPosition += Vector3.down * moveDownDistance;
            }
        }
    }

    bool IsFirstInRow()
    {
        // Simplificar - apenas verificar por posição em vez de usar OverlapBoxAll
        GameObject[] allInvaders = GameObject.FindGameObjectsWithTag("Invader");

        Vector3 myPosition = transform.position;
        float horizontalTolerance = 0.4f;

        foreach (GameObject invader in allInvaders)
        {
            if (invader != gameObject)
            {
                Vector3 otherPosition = invader.transform.position;

                // Verifica se está na mesma coluna e abaixo
                if (Mathf.Abs(otherPosition.x - myPosition.x) < horizontalTolerance &&
                    otherPosition.y < myPosition.y)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called with: " + other.tag);
        if (other.CompareTag("Missel"))
        {
            Debug.Log("Invader colidiu com um missel e será destruído");

            // Adiciona pontos com base no tipo do invasor
            int points = 0;
            switch (invaderType)
            {
                case 1:
                    points = 10;
                    break;
                case 2:
                    points = 20;
                    break;
                case 3:
                    points = 30;
                    break;
            }
            ScoreManager.Instance.AddScore(points);

            // Notificar o GameManager que um invasor foi destruído
            gameManager.InvaderDestroyed();

            Destroy(gameObject);
        }
        else if (other.CompareTag("InvaderMissel"))
        {
            Debug.Log("Invader colidiu com um missel do invasor e não será destruído");
            // Ignora colisões com mísseis do invasor
        }
    }

    // Gizmo para visualizar a área de detecção no editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * 0.5f, new Vector2(1f, 0.5f));
    }
}
