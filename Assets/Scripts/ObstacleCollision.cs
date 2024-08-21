using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with obstacle");
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Oyun sonu işlemler
    }
}
