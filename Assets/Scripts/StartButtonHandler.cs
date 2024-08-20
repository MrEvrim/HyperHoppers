using UnityEngine;
using System.Collections;

public class StartButtonHandler : MonoBehaviour
{
    public MonoBehaviour script1;  // Aktifleştirilecek ilk script
    public MonoBehaviour script2;  // Aktifleştirilecek ikinci script
    public Animator animator;   // Animasyonun bağlı olduğu animator
    public Transform targetPosition; // Kameranın taşınacağı hedef pozisyon
    public float moveDuration = 2.0f; // Hareket süresi
    public GameObject panel; // Kapatılacak panel

    public void OnStartButtonClicked()
    {
        // Scriptleri aktif hale getir
        script1.enabled = true;
        script2.enabled = true;

        // Animasyonun "Start" trigger'ını tetikle
        animator.SetTrigger("isStart");

        // Paneli kapat
        panel.SetActive(false);

        // Kamerayı yavaş yavaş hedef pozisyona taşı
        StartCoroutine(MoveCameraToPosition());
    }

    private IEnumerator MoveCameraToPosition()
    {
        float elapsedTime = 0;
        Vector3 startingPosition = Camera.main.transform.position;
        Quaternion startingRotation = Camera.main.transform.rotation;

        while (elapsedTime < moveDuration)
        {
            // Lerp the position
            Camera.main.transform.position = Vector3.Lerp(startingPosition, targetPosition.position, elapsedTime / moveDuration);

            // Lerp the rotation using Quaternion
            Camera.main.transform.rotation = Quaternion.Lerp(startingRotation, targetPosition.rotation, elapsedTime / moveDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and rotation are exact
        Camera.main.transform.position = targetPosition.position;
        Camera.main.transform.rotation = targetPosition.rotation;
    }
}
