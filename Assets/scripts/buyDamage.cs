using UnityEngine;

public class buyDamage : MonoBehaviour
{
    public GameStateManager gameState;
    public Dragonfly damage;

    void Start()
    {
        gameState = FindObjectOfType<GameStateManager>();
        damage = FindObjectOfType<Dragonfly>();
    }

    public void OnButtonClick()
    {
        Debug.Log("clicked");
        if (damage != null && gameState != null)
        {
            damage.damage += 0.5f;
            gameState.balance -= 50;
        }
        else
        {
            Debug.LogError("Scripts are not assigned.");
        }
    }
}
