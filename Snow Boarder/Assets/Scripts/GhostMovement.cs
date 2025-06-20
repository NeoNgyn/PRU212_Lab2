using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [Header("Di chuy?n ngang")]
    [Tooltip("T?c ?? di chuy?n c?a con ma.")]
    public float moveSpeed = 2f;
    [Tooltip("Con ma s? bay sang m?i b�n bao xa t? v? tr� b?t ??u.")]
    public float moveRange = 5f;

    [Header("Bay theo ??a h�nh")]
    [Tooltip("?? cao con ma bay l? l?ng tr�n m?t ??t.")]
    public float hoverHeight = 1.5f;
    [Tooltip("Ch?n Layer c?a m?t ??t ?? con ma c� th? 'nh�n' th?y.")]
    public LayerMask groundLayer;

    private Vector3 startPosition;
    private int moveDirection = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    // S? d?ng FixedUpdate ?? c�c t�nh to�n v?t l� ?n ??nh h?n
    void FixedUpdate()
    {
        HandleHorizontalMovement();
        HandleVerticalMovement();
    }

    // H�m x? l� di chuy?n ngang
    void HandleHorizontalMovement()
    {
        transform.Translate(Vector3.right * moveSpeed * moveDirection * Time.deltaTime);

        if (transform.position.x >= startPosition.x + moveRange)
        {
            moveDirection = -1;
            FlipSprite();
        }
        else if (transform.position.x <= startPosition.x - moveRange)
        {
            moveDirection = 1;
            FlipSprite();
        }
    }

    // H�m x? l� di chuy?n d?c theo ??a h�nh
    void HandleVerticalMovement()
    {
        // B?n m?t tia Raycast t? v? tr� con ma th?ng xu?ng d??i
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, groundLayer);

        // N?u tia Raycast ch?m v�o m?t v?t th? (c� collider v� thu?c groundLayer)
        if (hit.collider != null)
        {
            // C?p nh?t l?i v? tr� Y c?a con ma = v? tr� va ch?m + ?? cao l? l?ng
            // Gi? nguy�n v? tr� X v� Z c?a n�
            transform.position = new Vector3(transform.position.x, hit.point.y + hoverHeight, transform.position.z);
        }
    }

    void FlipSprite()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}