using UnityEngine;

public class EnemyDiagonalBullet : MonoBehaviour
{
    [SerializeField] private float bulletspeed = 15f;
    [SerializeField] private Vector2 ScreenSpawn = new Vector2(40f, 22f);

    void Start ()
    {
        
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
