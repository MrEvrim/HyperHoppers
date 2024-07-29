using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f; // Oyuncunun hareket hızı
    public float jumpForce = 10f; // Zıplama kuvveti
    public float laneDistance = 2f; // Şeritler arası mesafe

    private bool isGrounded; // Oyuncunun zeminde olup olmadığını kontrol eder
    private float horizontalInput; // Yatay girdi
    private int currentLane = 1; // Mevcut şerit (0: sol, 1: orta, 2: sağ)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Mobil giriş kontrolü
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    MoveLeft();
                }
                else
                {
                    MoveRight();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Jump();
            }
        }

        // Fare giriş kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Hareketi uygulama
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (currentLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (currentLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * speed);
    }

    private void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
        }
    }

    private void MoveRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
