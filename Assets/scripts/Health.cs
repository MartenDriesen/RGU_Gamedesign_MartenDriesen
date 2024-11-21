using System.Collections;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public TMP_Text health;
    public GameObject healthbar;

    public float currentHealth = 100;
    public float maxHealth = 100;
    private float maxScale = 1.5f;

    private float lastHealth;

    private SpriteRenderer headRenderer;
    private SpriteRenderer bodyRenderer;
    private SpriteRenderer frontArmRenderer;
    private SpriteRenderer backArmRenderer;
    private SpriteRenderer frontLegRenderer;
    private SpriteRenderer backLegRenderer;

    public Sprite headHit;
    public Sprite bodyHit;
    public Sprite frontArmHit;
    public Sprite backArmHit;
    public Sprite frontLegHit;
    public Sprite backLegHit;

    private Sprite originalHead;
    private Sprite originalBody;
    private Sprite originalFrontArm;
    private Sprite originalBackArm;
    private Sprite originalFrontLeg;
    private Sprite originalBackLeg;

    void Start()
    {
        headRenderer = GameObject.Find("head").GetComponent<SpriteRenderer>();
        bodyRenderer = GameObject.Find("body").GetComponent<SpriteRenderer>();
        frontArmRenderer = GameObject.Find("frontArm").GetComponent<SpriteRenderer>();
        backArmRenderer = GameObject.Find("backArm").GetComponent<SpriteRenderer>();
        frontLegRenderer = GameObject.Find("frontLeg").GetComponent<SpriteRenderer>();
        backLegRenderer = GameObject.Find("backLeg").GetComponent<SpriteRenderer>();

        originalHead = headRenderer.sprite;
        originalBody = bodyRenderer.sprite;
        originalFrontArm = frontArmRenderer.sprite;
        originalBackArm = backArmRenderer.sprite;
        originalFrontLeg = frontLegRenderer.sprite;
        originalBackLeg = backLegRenderer.sprite;

        lastHealth = currentHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        health.text = $"{currentHealth}/{maxHealth}";
        UpdateHealthBar();

        if (currentHealth < lastHealth)
        {
            StartCoroutine(ShowHitEffect());  }

        lastHealth = currentHealth;
    }

    private void UpdateHealthBar()
    {  float healthScale = Mathf.Clamp01(currentHealth / maxHealth); 
        float adjustedScale = healthScale * maxScale;

        healthbar.transform.localScale = new Vector3(adjustedScale, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }

    private IEnumerator ShowHitEffect()
    {
        headRenderer.sprite = headHit;
        bodyRenderer.sprite = bodyHit;
        frontArmRenderer.sprite = frontArmHit;
        backArmRenderer.sprite = backArmHit;
        frontLegRenderer.sprite = frontLegHit;
        backLegRenderer.sprite = backLegHit;

        yield return new WaitForSeconds(0.2f);

        headRenderer.sprite = originalHead;
        bodyRenderer.sprite = originalBody;
        frontArmRenderer.sprite = originalFrontArm;
        backArmRenderer.sprite = originalBackArm;
        frontLegRenderer.sprite = originalFrontLeg;
        backLegRenderer.sprite = originalBackLeg;
    }
}
