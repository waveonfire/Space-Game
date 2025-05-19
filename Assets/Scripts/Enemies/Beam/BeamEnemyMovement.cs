using UnityEngine;

public class BeamEnemyMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SpriteRenderer trailRenderer;
    [SerializeField] private float beamDistance = 45;
    [SerializeField] private float preBeamTime = 1; // Время свечения trail
    [SerializeField] private float beamActiveTime = 2f; // Время выстрела
    [SerializeField] private float fadeOutTime = 0.5f; // Время затухания
    [SerializeField] private GameObject explosionEffectPrefab;
    private Vector2 targetPosition;
    private bool isMoving = true;
    private float nextBeamTime;
    private bool isPreBeam;
    private bool isBeamActive;
    private bool isFading;
    private float preBeamTimer;

    private ScoreManager scoreManager;
    [SerializeField] private AudioSource shotSound;

    void Start ()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        shotSound.volume = 0.25f;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        lineRenderer.enabled = false;
        if (trailRenderer != null)
            trailRenderer.enabled = false;
    }

    public void SetPositions (Vector2 startPos, Vector2 target)
    {
        transform.position = startPos;
        targetPosition = target;
        isMoving = true;
    }

    void Update ()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
            return;
        }

        if (Time.time >= nextBeamTime)
        {
            if (!isPreBeam && !isBeamActive && !isFading)
            {
                StartPreBeam();
                preBeamTimer = 0f;
                nextBeamTime = Time.time + preBeamTime;
            }
            else if (isPreBeam && !isBeamActive)
            {
                StartBeam();
                nextBeamTime = Time.time + beamActiveTime;
            }
            else if (isBeamActive)
            {
                StartFade();
                nextBeamTime = Time.time + fadeOutTime;
            }
            else
            {
                StopFade();
                nextBeamTime = Time.time;
            }
        }

        if (isPreBeam && trailRenderer != null)
        {
            preBeamTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.1f, 1f, preBeamTimer / preBeamTime);
            trailRenderer.color = new Color(1f, 1f, 1f, alpha);
        }

        if (isFading && trailRenderer != null)
        {
            FadeAll();
        }
    }

    private void StartPreBeam ()
    {
        isPreBeam = true;
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
            trailRenderer.color = new Color(1f, 1f, 1f, 0.1f);
        }
    }

    private void StartBeam ()
    {
        isPreBeam = false;
        isBeamActive = true;
        lineRenderer.enabled = true;
        Color startColor = lineRenderer.startColor;
        Color endColor = lineRenderer.endColor;
        startColor.a = 1f;
        endColor.a = 1f;
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        UpdateBeam();
    }

    private void StartFade ()
    {
        isBeamActive = false;
        isFading = true;;
    }

    private void StopFade ()
    {
        isFading = false;
        lineRenderer.enabled = false;
        if (trailRenderer != null)
            trailRenderer.enabled = false;
    }

    private void UpdateBeam ()
    {
        Vector3 startPos = transform.position + new Vector3(0f, -2f, 0f);
        Vector3 endPos = startPos + Vector3.down * beamDistance;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
        shotSound.Play();
    }

    private void FadeAll ()
    {
        if (trailRenderer != null)
        {
            Color trailColor = trailRenderer.color;
            trailColor.a -= Time.deltaTime / fadeOutTime;
            trailRenderer.color = trailColor;
        }

        Color startColor = lineRenderer.startColor;
        Color endColor = lineRenderer.endColor;
        startColor.a -= Time.deltaTime / fadeOutTime;
        endColor.a -= Time.deltaTime / fadeOutTime;
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;

        if (startColor.a <= 0)
        {
            lineRenderer.enabled = false;
            if (trailRenderer != null)
                trailRenderer.enabled = false;
            isFading = false;
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
    private void OnDestroy ()
    {
        if (scoreManager != null) scoreManager.AddScore(20);
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