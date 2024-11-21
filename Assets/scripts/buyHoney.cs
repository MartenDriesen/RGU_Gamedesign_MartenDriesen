using UnityEngine;

public class buyHoney : MonoBehaviour
{
    public HiveLogic hiveLogic;
    public GameStateManager gameState;

    void Start()
    {
        hiveLogic = FindObjectOfType<HiveLogic>();
        gameState = FindObjectOfType<GameStateManager>();
    }

    public void OnButtonClick()
    {
        Debug.Log("clicked");
        if (hiveLogic != null && gameState != null)
        {
            hiveLogic.maxHoneypots += 1;
            gameState.balance -= 40;
        }
        else
        {
            Debug.LogError("Scripts are not assigned.");
        }
    }
}
