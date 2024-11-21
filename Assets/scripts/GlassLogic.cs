using System.Collections;
using UnityEngine;

public class GlassLogic : MonoBehaviour
{
    public Sprite stage0;
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;
    public GameObject player;
    public float totalBreakTime = 3.5f;
    public float respawnTime = 3f;
    public float healInterval = 1.5f;

    private float timeToNextStage;
    private float playerOnGlassTime = 0f;
    private float healTimer = 0f;
    private SpriteRenderer spriteRenderer;
    private bool playerOnGlass = false;
    private int currentStage = 0;
    private bool isBroken = false;

    void Start()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = stage0;
        timeToNextStage = totalBreakTime / 4f;
    }

    void Update()
    {
        if (playerOnGlass && !isBroken)
        {  playerOnGlassTime += Time.deltaTime;
            healTimer = 0f;

            if (playerOnGlassTime >= timeToNextStage * (currentStage + 1))
            {
                AdvanceBreakStage();}
        }
        else if (!playerOnGlass && !isBroken && currentStage > 0)
        {  healTimer += Time.deltaTime;

            if (healTimer >= healInterval)
            {    HealOneStage();
                healTimer = 0f;
            }
        }
    }

    private void AdvanceBreakStage()
    {
        currentStage++;

        switch (currentStage)
        {
            case 1:
                spriteRenderer.sprite = stage1;
                break;
            case 2:
                spriteRenderer.sprite = stage2;
                break;
            case 3:
                spriteRenderer.sprite = stage3;
                isBroken = true;
                StartRespawn();
                break;
        }
    }

    private void HealOneStage()
    {
        currentStage--;

        switch (currentStage)
        {
            case 0:
                spriteRenderer.sprite = stage0;
                break;
            case 1:
                spriteRenderer.sprite = stage1;
                break;
            case 2:
                spriteRenderer.sprite = stage2;
                break;
        }
    }

    private void StartRespawn()
    {  player.transform.SetParent(null);
        gameObject.SetActive(false);

        Invoke(nameof(RespawnGlass), respawnTime);
    }

    private void RespawnGlass()
    {
        gameObject.SetActive(true);
        ResetGlass();
    }

    private void ResetGlass()
    {
     isBroken = false;
        currentStage = 0;
    playerOnGlassTime = 0f;
        healTimer = 0f;
        spriteRenderer.sprite = stage0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {  if (collision.gameObject.CompareTag("player") && !isBroken)
      {
            playerOnGlass = true;
           collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
         playerOnGlass = false;
            playerOnGlassTime = 0f;
            collision.transform.SetParent(null);
        }
    }
}
