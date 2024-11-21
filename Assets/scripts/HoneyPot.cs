using UnityEngine;

public class HoneyPot : MonoBehaviour
{
    public int sellValue = 15;
    private GameStateManager gameStateManager;
    private HiveLogic hiveLogic;

    void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
        if (gameStateManager == null)
        {     Debug.LogError("GameStateManager not findable");
        }

        hiveLogic = FindObjectOfType<HiveLogic>();
        if (hiveLogic == null)
        {   Debug.LogError("HiveLogic not findable.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { if (collision.CompareTag("SellPad"))
        {
            SellHoney();
        }
    }

    private void SellHoney()
    { if (gameStateManager != null)
        {
            gameStateManager.AddToBalance(sellValue);
            Debug.Log("Honeypot sold for " + sellValue + "Current balance: " + gameStateManager.balance);

            if (hiveLogic != null) {
                hiveLogic.honeypotCount--;
                Debug.Log("Honeypot is less now : " + hiveLogic.honeypotCount);
            }
            
            Destroy(gameObject);
        }
        else
        {   Debug.LogError("GameStateManager  missing");
        }
    }
}
