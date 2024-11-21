using UnityEngine;

public class buyHealth : MonoBehaviour
{
    public GameStateManager gameState;
    public Health health;

    void Start()
    {
        gameState = FindObjectOfType<GameStateManager>();
        health = FindObjectOfType<Health>();
    }

    public void OnButtonClick()
    {
        Debug.Log("clicked");
        if (health != null && gameState != null)
        {
            health.maxHealth += 10f;
            gameState.balance -= 40;
        }
        else
        {
            Debug.LogError("Scripts are not assigned.");
        }
    }
}
