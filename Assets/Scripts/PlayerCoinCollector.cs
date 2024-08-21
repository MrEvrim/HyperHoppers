using UnityEngine;
using TMPro;

public class PlayerCoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int totalCoins = 0;

    void Start()
    {
        UpdateCoinText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            totalCoins++;
            UpdateCoinText();
            Destroy(other.gameObject); 
        }
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins: ¥" + totalCoins.ToString();
    }
}
