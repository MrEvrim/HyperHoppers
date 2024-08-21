using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private Move moveScript; 
    private float speed; 

    void Start()
    {
        moveScript = FindObjectOfType<Move>();
        if (moveScript != null)
        {
            speed = moveScript.groundSpeed; 
        }
        else
        {
            Debug.LogWarning("Move script not found in the scene.");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.back * -speed * Time.deltaTime);
    }
}
