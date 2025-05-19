using UnityEngine;

public class EasyEnemyMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private GameObject explosionEffectPrefab;
    private Vector2 targetPosition;
    private float nexttimeshoot;
    private bool isMoving = true;

    private Vector3 bulletPos;
    private ScoreManager scoreManager;
    void Start ()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
    public void SetPositions (Vector2 startPos, Vector2 target)
    {
        targetPosition = target;
    }

    void Update ()
    {
        if (nexttimeshoot >= fireRate)
        {
            Shoot();
            nexttimeshoot = 0;
        }
        nexttimeshoot += Time.fixedDeltaTime;
        if (!isMoving) return;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }
    private void Shoot ()
    {
        bulletPos = new Vector3(firePoint.position.x, firePoint.position.y - 1.35f);

        GameObject Bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);

        EnemyBullet BulletScript = Bullet.GetComponent<EnemyBullet>();

        BulletScript.SetDirection(Vector2.down);

    }
    private void OnDestroy ()
    {
        if (scoreManager != null) scoreManager.AddScore(10);
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(explosion, ps.main.duration); // Знищуємо ефект після завершення
            }
        }
        Destroy(gameObject);
    }
}
