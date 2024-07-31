using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f; // Oyuncunun hareket hızı
    public float laneDistance = 2f; // Şeritler arası mesafe
    public float swipeThreshold = 100f; // Kaydırma hareketi için gereken minimum mesafe

    private int currentLane = 1; // Mevcut şerit (0: sol, 1: orta, 2: sağ)

    private Rigidbody rb;
    private Vector2 startTouchPosition, swipeDelta;
    private bool isSwiping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        DetectSwipe();
    }

    private void FixedUpdate()
    {
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

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isSwiping = true;
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                swipeDelta = touch.position - startTouchPosition;

                if (swipeDelta.magnitude > swipeThreshold)
                {
                    ProcessSwipe(swipeDelta);
                    ResetSwipe();
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                ResetSwipe();
            }
        }

        // Fare girişleri için
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            swipeDelta = (Vector2)Input.mousePosition - startTouchPosition;

            if (swipeDelta.magnitude > swipeThreshold)
            {
                ProcessSwipe(swipeDelta);
                ResetSwipe();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ResetSwipe();
        }
    }

    private void ProcessSwipe(Vector2 swipeDelta)
    {
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x < 0)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }
        else
        {
            if (swipeDelta.y > 0)
            {
                Jump();
            }
            else if (swipeDelta.y < 0)
            {
                Crouch();
            }
        }
    }

    private void ResetSwipe()
    {
        startTouchPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
        isSwiping = false;
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
        animator.SetTrigger("isJump");
        Debug.Log("Jump detected");
    }

    private void Crouch()
    {
        animator.SetTrigger("isTrumple");
        Debug.Log("Crouch detected");
    }
}
