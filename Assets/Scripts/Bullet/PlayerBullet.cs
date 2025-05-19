using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float bulletspeed = 40f;
    [SerializeField] private Vector2 ScreenSpawn = new Vector2(39f, 22.5f);
    
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector2.up * bulletspeed * Time.deltaTime);

        if (transform.position.y > ScreenSpawn.y)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            Destroy(enemy);
            Destroy(gameObject);
        }
    }
}
