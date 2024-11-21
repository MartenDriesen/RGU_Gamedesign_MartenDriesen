using UnityEngine;

public class Dragonfly : MonoBehaviour
{
    public float damage = 1f;
    public float currentSpeed = 3f;
    public float angrySpeed = 5f;
    private float hitsTaken = 0f;

    public float dragonflyHealth = 5f;
    public bool isMoving = true;
    public bool isHit = false;

    private float flipCooldownTime = 1f;
    private float flipCooldownTimer = 0f;

    public bool movingRight = true;
    private float damageCooldownTime = 1.5f;
    private float damageCooldownTimer = 0f;

    public GameObject DragonflyHoneyPot;
    private Health health;
    public GameObject player;

    public float hitCooldownTime = 0.1f;
    private float hitCooldownTimer = 0f;

    public bool playerInvi = false; 

    void Start()
    {
        health = FindObjectOfType<Health>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }
    }

    void Update()
    {
        if (damageCooldownTimer > 0f)
            damageCooldownTimer -= Time.deltaTime;

        if (flipCooldownTimer > 0f)
            flipCooldownTimer -= Time.deltaTime;

        if (hitCooldownTimer > 0f)
            hitCooldownTimer -= Time.deltaTime;

        
        if (playerInvi)
        { isMoving = false;
        }

        
        if (!playerInvi)
        {
            if (isHit && player != null) 
            {
                Vector3 targetPosition = player.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, angrySpeed * Time.deltaTime);

                if (transform.position.x < player.transform.position.x)
                {  transform.localScale = new Vector3(-1.9f, 1.9f, 1.9f);
                }
                else
                {   transform.localScale = new Vector3(1.9f, 1.9f, 1.9f);
                }

                if (Vector3.Distance(transform.position, player.transform.position) < 1f)
                {   TryDamagePlayer();
                }
            }
            else if (isMoving && !isHit)
            { float moveDirection = movingRight ? 1f : -1f;
                transform.Translate(Vector2.right * moveDirection * currentSpeed * Time.deltaTime);

                if (movingRight)
                { transform.localScale = new Vector3(-1.9f, 1.9f, 1.9f);
                }
                else
                { transform.localScale = new Vector3(1.9f, 1.9f, 1.9f);}
            }
        }

        if (hitsTaken >= dragonflyHealth)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player") && hitCooldownTimer <= 0f)
        {  TryDamagePlayer();
            hitCooldownTimer = hitCooldownTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            TryDamagePlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Projectile") && hitCooldownTimer <= 0f)
        {
      isHit = true;
         hitsTaken += damage;
            hitCooldownTimer = hitCooldownTime;
            Destroy(collider.gameObject);
        }

        if (collider.CompareTag("ground") && flipCooldownTimer <= 0f)
        {
            movingRight = !movingRight; flipCooldownTimer = flipCooldownTime;
     }
    }

    private void TryDamagePlayer()
    {
        if (damageCooldownTimer <= 0f && health != null)
        {
       health.currentHealth -= 20;
         damageCooldownTimer = damageCooldownTime;  
 }
    }

 private void Die()
    {
        if (DragonflyHoneyPot != null)
        {             Instantiate(DragonflyHoneyPot, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
