using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private Move moveScript; // Move script referansı
    private float speed; // Engellerin hareket hızı

    void Start()
    {
        // Move script'ini bul ve hız değerini al
        moveScript = FindObjectOfType<Move>();
        if (moveScript != null)
        {
            speed = moveScript.groundSpeed; // Move script'indeki groundSpeed değerini al
        }
        else
        {
            Debug.LogWarning("Move script not found in the scene.");
        }
    }

    void Update()
    {
        // Z yönünde hareket et
        transform.Translate(Vector3.back * -speed * Time.deltaTime);
    }
}
