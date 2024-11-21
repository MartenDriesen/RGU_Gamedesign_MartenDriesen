using UnityEngine;

public class buyInventory : MonoBehaviour
{
    public PickupObjectsLogic pickupObjectsLogic;
    public GameStateManager gameState;

    void Start()
    {
        pickupObjectsLogic = FindObjectOfType<PickupObjectsLogic>();
        gameState = FindObjectOfType<GameStateManager>();
    }

    public void OnButtonClick()
    {
        Debug.Log("clicked");
        if (pickupObjectsLogic != null && gameState != null)
        {
            if (pickupObjectsLogic.maxPickupCount <= 3)
            {
                pickupObjectsLogic.maxPickupCount += 1;
                gameState.balance -= 75;
            }
        }
        else
        {
            Debug.LogError("Scripts are not assigned.");
        }
    }
}
