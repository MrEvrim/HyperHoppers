using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f; 
    public float laneDistance = 2f; 
    public float swipeThreshold = 100f; 
    public float jumpHeight = 2f; 
    public float jumpDuration = 0.5f; 

    public MonoBehaviour gameManager;

    private int currentLane = 1; 
    private Rigidbody rb;
    private Vector2 startTouchPosition, swipeDelta;
    private bool isSwiping;
    public bool isDead = false; 
    private bool isJumping = false; 

    public StartButtonHandler startButtonHandler;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startButtonHandler = FindObjectOfType<StartButtonHandler>();
    }

    void Update()
    {
        if (!isDead)
        {
            DetectSwipe();
        }
    }

    private void FixedUpdate()
    {
        if (!isDead && !isJumping) 
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
        if (!isJumping) // Oyuncu zaten zıplamıyorsa
        {
            animator.SetTrigger("isJump");
            StartCoroutine(JumpRoutine());
        }
    }

    private System.Collections.IEnumerator JumpRoutine()
    {
        isJumping = true;
        Vector3 startPos = transform.position;
        Vector3 jumpApex = new Vector3(startPos.x, startPos.y + jumpHeight, startPos.z);

        float elapsedTime = 0;

        // Yükseliş
        while (elapsedTime < jumpDuration / 2f)
        {
            transform.position = Vector3.Lerp(startPos, jumpApex, (elapsedTime / (jumpDuration / 2f)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        // Düşüş
        while (elapsedTime < jumpDuration / 2f)
        {
            transform.position = Vector3.Lerp(jumpApex, startPos, (elapsedTime / (jumpDuration / 2f)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos; // Pozisyonu tam olarak başlangıç noktasına getir
        isJumping = false;
    }

    private void Crouch()
    {
        animator.SetTrigger("isTrumple");
        Debug.Log("Crouch detected");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            isDead = true; 
            animator.SetTrigger("isDead"); 
            Debug.Log("Player has died.");

            Time.timeScale = 0f;
        }
    }
}
