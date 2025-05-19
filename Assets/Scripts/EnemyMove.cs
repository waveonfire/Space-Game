using UnityEngine;

public class EnemyMove: MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 1.5f;
    private Vector2 targetPosition;
    private float nexttimeshoot;
    private bool isMoving = true;

    private Vector3 firstFirePoint;
    private Vector3 secondFirePoint;
    void Start ()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
    public void SetPositions (Vector2 target)
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
        firstFirePoint = new Vector3(firePoint.position.x + 0.5f, firePoint.position.y - 0.1f);
        secondFirePoint = new Vector3(firePoint.position.x - 0.5f, firePoint.position.y - 0.1f);

        GameObject FirstBullet = Instantiate(bulletPrefab, firstFirePoint, Quaternion.identity);
        GameObject SecondBullet = Instantiate(bulletPrefab, secondFirePoint, Quaternion.identity);

        EnemyBullet FirstBulletScript = FirstBullet.GetComponent<EnemyBullet>();
        EnemyBullet SecondBulletScript = SecondBullet.GetComponent<EnemyBullet>();

    }
}
