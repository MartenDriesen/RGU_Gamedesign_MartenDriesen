using UnityEngine;

public class BuyButtonController : MonoBehaviour
{
    public GameStateManager gameState;
    public MoveSprite moveSprite;

    void Start()
    {
        gameState = FindObjectOfType<GameStateManager>();
        moveSprite = FindObjectOfType<MoveSprite>();
    }

    public void OnButtonClick()
    {
        Debug.Log("clicked");
        if (moveSprite != null && gameState != null)
        {
            moveSprite.sprintSpeed += 0.4f;
            gameState.balance -= 20;
        }
        else
        {
            Debug.LogError("Scripts are not assigned.");
        }
    }
}
