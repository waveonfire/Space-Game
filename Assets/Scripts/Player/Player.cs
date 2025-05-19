using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float playerMoveSpeed = 25f;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private Vector3 firstFirePoint;
    private Vector3 secondFirePoint;

    [SerializeField] private AudioSource shotSound;


    private float nexttimeshoot;

    private PlayerInputActions playerInputActions;

    private Rigidbody2D rb;

    private void Awake ()
    {
        shotSound.volume = 0.45f;
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
    private Vector2 GetMovementVector ()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
    private void Update ()
    {
        Vector2 inputVector = GetMovementVector();

        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (playerMoveSpeed * Time.fixedDeltaTime));

        if (Input.GetKey(KeyCode.Space) && nexttimeshoot >= fireRate)
        {
            shotSound.Play();
            Shoot();
            nexttimeshoot = 0;
        }
        nexttimeshoot += Time.fixedDeltaTime;
    }
    public void OnDestroy ()
    {
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
    private void Shoot ()
    {
        firstFirePoint = new Vector3(firePoint.position.x + 0.5f, firePoint.position.y + 0.1f);
        secondFirePoint = new Vector3(firePoint.position.x - 0.5f, firePoint.position.y + 0.1f);

        GameObject FirstBullet = Instantiate(bulletPrefab, firstFirePoint, Quaternion.identity);
        GameObject SecondBullet = Instantiate(bulletPrefab, secondFirePoint, Quaternion.identity);

        PlayerBullet FirstBulletScript = FirstBullet.GetComponent<PlayerBullet>();
        PlayerBullet SecondBulletScript = SecondBullet.GetComponent<PlayerBullet>();

    }
}
