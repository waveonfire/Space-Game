using UnityEngine;

public class KamikadzeMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private float speed = 14f;
    [SerializeField] private GameObject explosionEffectPrefab;
    private Vector2 targetPosition;
    private ScoreManager scoreManager;

    void Start ()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    public void SetPositions (Vector2 startPos, Vector2 target)
    {
        targetPosition = target;
    }

    void Update ()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject enemy = other.gameObject;
            Destroy(enemy);
            Destroy(gameObject);
        }
    }
    public void OnDestroy()
    {
        if (scoreManager != null && transform.position.y > -22)
            scoreManager.AddScore(10);
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(explosion, ps.main.duration);
            }
        }
        Destroy(gameObject);

    }
}
