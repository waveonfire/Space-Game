using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletspeed = 15f;
    [SerializeField] private Vector2 ScreenSpawn = new Vector2(39.5f, 22f);

    private Vector2 direction;
    void Start ()
    {

    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }
    void Update ()
    {
        transform.Translate(Vector2.down * bulletspeed * Time.deltaTime);

        if (transform.position.y < -ScreenSpawn.y || Mathf.Abs(transform.position.x) > ScreenSpawn.x)
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
}
