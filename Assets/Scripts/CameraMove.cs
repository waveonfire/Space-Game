using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 5.0f;
    private Vector3 cameraStartPos;
    private float repeatWidth;
   

    void Start ()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        cameraStartPos = transform.position;
        repeatWidth = (collider.size.y * transform.localScale.y) / 2;
    }
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * cameraSpeed);
        if (transform.position.y <= cameraStartPos.y - repeatWidth)
        {
            transform.position = cameraStartPos;
        }
    }
}
