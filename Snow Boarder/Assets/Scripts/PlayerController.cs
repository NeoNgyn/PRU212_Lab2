using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] float moveSpeed = 10f;      // Tốc độ di chuyển
    [SerializeField] float boostSpeed = 20f;     // Tốc độ tăng tốc khi giữ Shift

    [Header("Flip Settings")]
    [SerializeField] float flipDuration = 0.5f;  // Thời gian để lộn 1 vòng
    [SerializeField] float flipAngle = 360f;     // Góc quay tròn khi lộn
    [SerializeField] float jumpForce = 7f;       // Lực nhảy lên

    Rigidbody2D rb2d;               
    SurfaceEffector2D surfaceEffector2D; // Hiệu ứng trượt trên mặt phẳng
    bool canMove = true;            // Check có đang điều khiển nhân vật ko
    bool isFlipping = false;        // CHeck có đang lộn hoặc quay ko

    void Start()
    {
        // Lấy component Rigidbody2D của player
        rb2d = GetComponent<Rigidbody2D>();

        // Tìm SurfaceEffector2D trong scene để thay đổi tốc độ trượt
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update()
    {
        // Chết thì reset
        if (!canMove) return;

        // Điều chỉnh tốc độ khi giữ Shift
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? boostSpeed : moveSpeed;

        // Di chuyển trái phải
        MovePlayerLeftRight();

        // Thay đổi tốc độ trượt trên mặt phẳng
        RespondToBoost();

        // Lộn quang trục Z khi nhấn Lên - XUống
        RespondToFlipZ();

        // Lộng quanh truc X khi nhấn Space hoặc kết hợp Lên - XUống
        RespondToFlipXCombo();

        // Lộn quanh trục Y khi nhấn Enter hoặc kết hợp Lên - XUống lộn chong chóng tre
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SpinCombo());
        }
    }

    // Chặn điều khiển khi chết
    public void DisableControls()
    {
        canMove = false;
    }

    // Di chuyển trái hoặc phải khi giữ phím mũi tên
    void MovePlayerLeftRight()
    {
        float horizontalInput = 0f;

        // Nhấn phím trái, di chuyển trái và lật hướng nhân vật sang trái
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
            RotateToLeft();
        }
        // Nhấn phím phải, di chuyển phải và lật hướng nhân vật sang phải
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
            RotateToRight();
        }

        // Thiết lập vận tốc tuyến tính của nhân vật theo hướng ngang và tốc độ hiện tại
        rb2d.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb2d.linearVelocity.y);
    }

    // Quay trái
    void RotateToLeft()
    {
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    // Quay phải
    void RotateToRight()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    // Giữ Shift để tăng tốc
    void RespondToBoost()
    {
        surfaceEffector2D.speed = Input.GetKey(KeyCode.LeftShift) ? boostSpeed : moveSpeed;
    }

    // Kiểm tra và bắt đầu lộn quanh trục Z khi nhấn phím lên hoặc xuống
    void RespondToFlipZ()
    {
        // Nếu đang lộn rồi thì không thực hiện lộn tiếp
        if (isFlipping) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(FlipZ(-1)); // Lộn 360 độ theo chiều kim đồng hồ 
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(FlipZ(1));  // Lộn 360 độ ngược chiều kim đồng hồ 
        }
    }

    // Kiểm tra và bắt đầu combo lộn quanh trục X khi nhấn Space (có thể kết hợp Up/Down)
    void RespondToFlipXCombo()
    {
        if (isFlipping) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FlipXCombo());
        }
    }

    // Coroutine thực hiện lộn 360 độ quanh trục Z
    IEnumerator FlipZ(int direction)
    {
        isFlipping = true; // Đánh dấu đang lộn

        // Nhảy lên với lực nhảy đã định
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Đợi 0.05s để nhân vật bật lên rồi mới quay
        yield return new WaitForSeconds(0.05f);

        // Biến đếm tổng góc đã quay
        float totalRotation = 0f;

        // Góc quay mỗi frame, căn cứ trên tổng thời gian lộn và flipAngle
        float rotationPerFrame = (flipAngle / flipDuration) * Time.deltaTime;

        // Lặp đến khi quay đủ 360 độ
        while (totalRotation < flipAngle)
        {
            float rotateAmount = rotationPerFrame;
            // Quay quanh trục Z với chiều direction (+1 hoặc -1)
            transform.Rotate(0f, 0f, rotateAmount * direction);

            totalRotation += rotateAmount;
            yield return null; // Đợi frame tiếp theo
        }

        isFlipping = false; // Kết thúc lộn, cho phép thao tác khác
    }

    // Coroutine thực hiện combo lộn 360 độ quanh trục X (kết hợp phím Up/Down)
    IEnumerator FlipXCombo()
    {
        isFlipping = true;

        // Nhảy lên
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.05f);

        float direction = 0f;

        // Xác định chiều quay dựa trên phím Up hoặc Down đang giữ
        if (Input.GetKey(KeyCode.UpArrow)) direction = -1f;  // Quay về phía trước
        else if (Input.GetKey(KeyCode.DownArrow)) direction = 1f; // Quay về phía sau
        else direction = -1f;  // Mặc định quay về phía trước nếu không giữ phím

        float totalRotation = 0f;
        float rotationPerFrame = (flipAngle / flipDuration) * Time.deltaTime;

        while (totalRotation < flipAngle)
        {
            float rotateAmount = rotationPerFrame;
            // Quay quanh trục X
            transform.Rotate(rotateAmount * direction, 0f, 0f);

            totalRotation += rotateAmount;
            yield return null;
        }

        isFlipping = false;
    }

    // Coroutine thực hiện combo quay chong chóng kết hợp xoay quanh trục X và Y (khi nhấn Enter)
    IEnumerator SpinCombo()
    {
        isFlipping = true;

        // Nhảy lên
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.05f);

        float directionX = 0f;

        // Xác định chiều quay quanh trục X dựa trên phím Up hoặc Down đang giữ
        if (Input.GetKey(KeyCode.UpArrow)) directionX = -1f;
        else if (Input.GetKey(KeyCode.DownArrow)) directionX = 1f;

        float totalRotation = 0f;
        float rotationPerFrame = (flipAngle / flipDuration) * Time.deltaTime;

        while (totalRotation < flipAngle)
        {
            float rotateAmount = rotationPerFrame;

            // Xoay quanh trục X và trục Y tạo combo quay chong chóng
            transform.Rotate(rotateAmount * directionX, rotateAmount, 0f);

            totalRotation += rotateAmount;
            yield return null;
        }

        isFlipping = false;
    }
}
