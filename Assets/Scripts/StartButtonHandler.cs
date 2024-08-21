using UnityEngine;
using System.Collections;

public class StartButtonHandler : MonoBehaviour
{
    public MonoBehaviour script1;  
    public MonoBehaviour script2;  
    public MonoBehaviour script3;
    public MonoBehaviour script4;
    public Animator animator;  
    public Transform targetPosition;
    public Transform deadTargetPosition;
    public float moveDuration = 2.0f; 
    public GameObject panel; 
    private PlayerMove playerMove;

    public void OnStartButtonClicked()
    {
        script1.enabled = true;
        script2.enabled = true;
        script3.enabled = true;
        script4.enabled = true;

        animator.SetTrigger("isStart");

        panel.SetActive(false);

        StartCoroutine(MoveCameraToPosition());

        playerMove = FindObjectOfType<PlayerMove>();
    }

    private IEnumerator MoveCameraToPosition()
    {
        float elapsedTime = 0;
        Vector3 startingPosition = Camera.main.transform.position;
        Quaternion startingRotation = Camera.main.transform.rotation;

        while (elapsedTime < moveDuration)
        {
            Camera.main.transform.position = Vector3.Lerp(startingPosition, targetPosition.position, elapsedTime / moveDuration);

            Camera.main.transform.rotation = Quaternion.Lerp(startingRotation, targetPosition.rotation, elapsedTime / moveDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = targetPosition.position;
        Camera.main.transform.rotation = targetPosition.rotation;
    }

    private void Update()
    {
        if (playerMove != null && playerMove.isDead)
        {
            Camera.main.transform.position = deadTargetPosition.position;
            Camera.main.transform.rotation = deadTargetPosition.rotation;
        }
    }
}
