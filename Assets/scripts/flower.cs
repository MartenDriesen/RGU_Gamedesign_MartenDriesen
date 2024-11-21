using UnityEngine;

public class Flower : MonoBehaviour
{
    public int sellValue = 2;
    private GameStateManager gameStateManager;

    void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
        if (gameStateManager == null)
        {   Debug.LogError("GameStateManager not findable");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SellPad"))
        {     SellFlower();
        }
    }

    private void SellFlower()
    {
        if (gameStateManager != null)  {
            gameStateManager.AddToBalance(sellValue);
            Debug.Log("Flower sold for " + sellValue + "! Current balance: " + gameStateManager.balance);
            Destroy(gameObject);
        }  else
        {   Debug.LogError("GameStateManager missing!");
        }
    }
}
