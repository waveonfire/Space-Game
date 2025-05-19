using UnityEngine;

public class EasyEnemyShoot : MonoBehaviour
{
    [SerializeField] private float bulletspeed = 15f;
    [SerializeField] private Vector2 ScreenSpawn = new Vector2(39f, 23f);

    void Start()
    {
        transform.Translate(Vector2.down * bulletspeed * Time.deltaTime);

        if (transform.position.y < -ScreenSpawn.y)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
